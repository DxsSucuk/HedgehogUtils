using EntityStates;
using RoR2;
using RoR2.Audio;
using RoR2.Skills;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace HedgehogUtils.Boost.EntityStates
{
    public class BoostEnter : BaseState
    {
        private ICharacterFlightParameterProvider flight;

        public override void OnEnter()
        {
            base.OnEnter();
            if (base.isAuthority)
            {
                flight = base.characterBody.GetComponent<ICharacterFlightParameterProvider>();
                if (base.characterMotor.isGrounded || Helpers.Flying(flight))
                {
                    if (base.inputBank.moveVector == Vector3.zero)
                    {
                        EnterBoostIdle();
                    }
                    else
                    {
                        EnterBoost();
                    }
                }
                else
                {
                    EnterAirBoost();
                }
            }
        }

        public virtual void EnterBoostIdle()
        {
            outer.SetNextState(EntityStateCatalog.InstantiateState(typeof(BoostIdle)));
        }

        public virtual void EnterBoost()
        {
            outer.SetNextState(EntityStateCatalog.InstantiateState(typeof(Boost)));
        }

        public virtual void EnterAirBoost()
        {
            Boost airBoost = (Boost)EntityStateCatalog.InstantiateState(typeof(Boost));
            airBoost.airBoosting = true;
            outer.SetNextState(airBoost);
        }
    }
}