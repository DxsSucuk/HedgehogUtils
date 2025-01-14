using EntityStates;
using R2API;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UIElements.Experimental;

namespace HedgehogUtils.Boost.EntityStates
{
    public class Boost : GenericCharacterMain
    {
        public const float minDuration = 0.4f;
        public const float minAirBoostDuration = 0.2f;
        public const float maxAirBoostDuration = 0.4f;
        
        public static float boostMeterDrain = 0.88f;
        public static float screenShake = 3.5f;
        public static float airBoostY = 8;

        public static string boostSoundString = "Play_boost";
        public static string boostChangeSoundString = "Play_boost_change";

        protected Vector3 targetDirection;
        public BoostLogic boostLogic;

        private TemporaryOverlayInstance temporaryOverlay;

        protected ICharacterFlightParameterProvider flight;

        protected static float boostCameraDistance = -13;
        private CharacterCameraParamsData boostingCameraParams = new CharacterCameraParamsData
        {
            maxPitch = 70f,
            minPitch = -70f,
            pivotVerticalOffset = 1.1f,
            idealLocalCameraPos = new Vector3(0f, 0f, boostCameraDistance),
            wallCushion = 0.1f
        };
        private CameraTargetParams.CameraParamsOverrideHandle camOverrideHandle;

        protected GameObject aura;

        protected bool drainBoostMeter = true;

        protected BuffDef buff;

        protected float maxRadiansTurnPerSecond = 3f;

        public bool airBoosting = false;

        public override void OnEnter()
        {
            base.OnEnter();
            base.characterBody.skillLocator.utility.onSkillChanged += OnSkillChanged;
            flight = base.characterBody.GetComponent<ICharacterFlightParameterProvider>();

            base.GetModelAnimator().SetBool("isBoosting", true);

            boostLogic = GetComponent<BoostLogic>();
            if (NetworkServer.active)
            {
                if (buff == null) { Log.Error($"Boost state of type \"{this.GetType()}\" does not have a buff defined. Make sure to define the buff BEFORE base.OnEnter()"); }
                else { base.characterBody.AddBuff(buff); }
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.25f);
            }

            if (airBoosting)
            {
                base.PlayCrossfade("Body", "AirBoost", "Roll.playbackRate", minAirBoostDuration, minAirBoostDuration / 3f);
                base.skillLocator.utility.DeductStock(1);
            }
            else
            {
                base.PlayCrossfade("Body", "Boost", 0.1f);
            }

            if (base.inputBank.moveVector != Vector3.zero)
            {
                base.characterDirection.forward = base.inputBank.moveVector;
            }

            targetDirection = base.characterDirection.forward;

            this.camOverrideHandle = base.cameraTargetParams.AddParamsOverride(new CameraTargetParams.CameraParamsOverrideRequest
            {
                cameraParamsData = boostingCameraParams,
                priority = 1f
            }, minDuration / 2f);

            CreateBoostVFX();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (drainBoostMeter)
            {
                if (NetworkServer.active)
                {
                    boostLogic.RemoveBoost(boostMeterDrain);
                }
                boostLogic.boostDraining = true;
            }

            base.characterBody.isSprinting = true;

            if (airBoosting)
            {
                if (!base.isGrounded)
                {
                    if (!Helpers.Flying(flight))
                    {
                        base.characterMotor.velocity.y = Mathf.Max(airBoostY, base.characterMotor.velocity.y);
                    }
                }
                else
                {
                    airBoosting = false;
                }
                if (base.fixedAge > maxAirBoostDuration)
                {
                    airBoosting = false;
                }
                if (base.isAuthority && (base.fixedAge > minAirBoostDuration && !base.inputBank.skill3.down))
                {
                    outer.SetNextStateToMain();
                    return;
                }
            }
            else if (base.isAuthority && base.fixedAge > minDuration && !base.inputBank.skill3.down)
            {
                outer.SetNextStateToMain();
                return;
            }

            if (base.isAuthority && (boostLogic.boostMeter <= 0 || !boostLogic.boostAvailable))
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override void HandleMovements()
        {
            if (base.isAuthority)
            {
                MoveDirection();
                this.ProcessJump();
            }
        }

        protected virtual void MoveDirection()
        {
            if (this.hasCharacterMotor)
            {
                if (this.moveVector != Vector3.zero)
                {
                    targetDirection = Vector3.RotateTowards(targetDirection, this.moveVector, maxRadiansTurnPerSecond * Time.fixedDeltaTime, 0.1f);
                }
                base.characterMotor.moveDirection = targetDirection;
                if (base.hasCharacterDirection)
                {
                    this.characterDirection.moveVector = base.characterMotor.moveDirection;
                }
            }
        }


        protected virtual void CreateBoostVFX()
        {
            Util.PlaySound(GetSoundString(), base.gameObject);
            if (base.isAuthority)
            {
                base.AddRecoil(-1f * screenShake, 1f * screenShake, -0.5f * screenShake, 0.5f * screenShake);
                EffectManager.SimpleMuzzleFlash(GetFlashPrefab(), base.gameObject, "MainHurtbox", true);
            }

            CreateTemporaryOverlay();

            if (GetAuraPrefab())
            {
                aura = GameObject.Instantiate<GameObject>(GetAuraPrefab(), base.FindModelChild("MainHurtbox"));
            }
        }

        protected virtual void RemoveBoostVFX()
        {
            RemoveTemporaryOverlay();

            if (aura)
            {
                Destroy(aura);
            }
        }

        private void CreateTemporaryOverlay()
        {
            if (!GetOverlayMaterial()) { return; }
            if (temporaryOverlay != null && temporaryOverlay.ValidateOverlay()) { return; }
            Transform modelTransform = base.GetModelTransform();
            if (!modelTransform) { return; }
            CharacterModel model = modelTransform.GetComponent<CharacterModel>();
            if (model)
            {
                temporaryOverlay = TemporaryOverlayManager.AddOverlay(model.gameObject);
                temporaryOverlay.originalMaterial = GetOverlayMaterial();
                temporaryOverlay.animateShaderAlpha = true; // Why does animateShaderAlpha make the entire TemporaryOverlayManager (and by extension the ENTIRE GAME) break when I try to destroy an overlay
                temporaryOverlay.duration = 0.2f;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 2f, 3f, 0.4f);
                temporaryOverlay.destroyComponentOnEnd = false;
                temporaryOverlay.destroyObjectOnEnd = false;
                temporaryOverlay.inspectorCharacterModel = model;
                temporaryOverlay.Start();
            }
        }

        private void RemoveTemporaryOverlay()
        {
            if (temporaryOverlay != null)
            {
                temporaryOverlay.animateShaderAlpha = false; // this is necessary trust me
                temporaryOverlay.Destroy();
            }
        }

        public override void OnExit()
        {
            base.GetModelAnimator().SetBool("isBoosting", false);
            boostLogic.boostDraining = false;
            base.cameraTargetParams.RemoveParamsOverride(this.camOverrideHandle, 1f);
            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(buff);
            }
            RemoveBoostVFX();

            base.characterBody.skillLocator.utility.onSkillChanged -= OnSkillChanged;
            base.OnExit();
        }

        public virtual string GetSoundString()
        {
            return "Play_boost";
        }

        public virtual GameObject GetFlashPrefab()
        {
            return Assets.powerBoostFlashEffect;
        }
        public virtual GameObject GetAuraPrefab()
        {
            return Assets.powerBoostAuraEffect;
        }
        public virtual Material GetOverlayMaterial()
        {
            return LegacyResourcesAPI.Load<Material>("Materials/matOnHelfire");
        }

        public virtual void OnSkillChanged(GenericSkill skill)
        {
            if (typeof(BoostEnter).IsAssignableFrom(skill.activationState.stateType))
            {
                outer.SetNextState(EntityStateCatalog.InstantiateState(skill.activationState.stateType));
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Pain;
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(airBoosting);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            airBoosting = reader.ReadBoolean();
        }
    }
}