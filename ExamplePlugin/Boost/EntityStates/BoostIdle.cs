using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HedgehogUtils.Boost.EntityStates
{
    public class BoostIdle : BaseSkillState
    {
        public Type boostEntityStateType;
        protected ICharacterFlightParameterProvider flight;
        public override void OnEnter()
        {
            base.OnEnter();
            flight = base.characterBody.GetComponent<ICharacterFlightParameterProvider>();
            base.PlayCrossfade("Body", "BoostIdleEnter", 0.3f);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.isAuthority)
            {
                if (base.inputBank.moveVector != Vector3.zero)
                {
                    outer.SetNextState(EntityStateCatalog.InstantiateState(boostEntityStateType));
                    return;
                }
                if (!base.inputBank.skill3.down || (!base.characterMotor.isGrounded && !Helpers.Flying(flight)))
                {
                    outer.SetNextStateToMain();
                    return;
                }
            }

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Pain;
        }
    }
}
