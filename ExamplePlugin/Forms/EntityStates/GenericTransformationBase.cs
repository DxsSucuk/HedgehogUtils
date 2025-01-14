using EntityStates;
using RoR2;
using RoR2.Audio;
using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace HedgehogUtils.Forms.EntityStates
{
    public abstract class GenericTransformationBase : TransformationBase
    {
        protected abstract float duration
        {
            get;
        }

        protected bool effectFired = false;

        public override void OnEnter()
        {
            base.OnEnter();
            if (base.isAuthority)
            {
                this.superSonic.superSonicState.SetNextStateToMain(); // detransform
            }
            if (duration > 0)
            {
                if (BodyCatalog.GetBodyName(base.characterBody.bodyIndex) == "SonicTheHedgehog")
                {
                    base.PlayAnimation("FullBody, Override", "Transform", "Roll.playbackRate", this.duration);
                }
                if (NetworkServer.active)
                {
                    base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, duration, 1);
                }
            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (fixedAge >= this.duration / 2 && !effectFired && this.superSonic)
            {
                Transform();
            }
           
            
            if (fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }

            if (base.characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
            }
        }

        public override void Transform()
        {
            effectFired = true;
            base.Transform();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Vehicle;
        }
    }
}