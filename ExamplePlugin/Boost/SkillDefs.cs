using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using RoR2;
using RoR2.Skills;
using EntityStates;
using UnityEngine;

namespace HedgehogUtils.Boost
{
    public class SkillDefs
    {
        public interface IBoostSkill
        {
            public SerializableEntityStateType boostIdleState { get; set; }
            public SerializableEntityStateType brakeState { get; set; }
            public Color boostHUDColor { get; set; }
        }

        public class BoostSkillDef : SkillDef, IBoostSkill
        {
            public SerializableEntityStateType boostIdleState { get; set; }
            public SerializableEntityStateType brakeState { get; set; }

            public Color boostHUDColor { get; set; }

            public override EntityState InstantiateNextState([NotNull] GenericSkill skillSlot)
            {
                if (!skillSlot.characterBody || !skillSlot.characterBody.characterMotor) { return base.InstantiateNextState(skillSlot); }
                return DetermineNextBoostState(skillSlot, activationState, boostIdleState);
            }
            public static EntityState DetermineNextBoostState([NotNull] GenericSkill skillSlot, SerializableEntityStateType boost, SerializableEntityStateType boostIdle)
            {
                ICharacterFlightParameterProvider flight = skillSlot.GetComponent<ICharacterFlightParameterProvider>();
                if (skillSlot.characterBody.characterMotor.isGrounded || Helpers.Flying(flight))
                {
                    if (skillSlot.characterBody.inputBank.moveVector == Vector3.zero)
                    {
                        return InstantiateBoostIdle(skillSlot, boostIdle);
                    }
                    else
                    {
                        return InstantiateBoost(skillSlot, boost);
                    }
                }
                else
                {
                    return InstantiateAirBoost(skillSlot, boost);
                }
            }

            public static EntityState InstantiateBoostIdle(GenericSkill skillSlot, SerializableEntityStateType boostIdle)
            {
                EntityState entityState = EntityStateCatalog.InstantiateState(boostIdle.stateType);
                ISkillState skillState = entityState as ISkillState;
                if (skillState != null)
                {
                    skillState.activatorSkillSlot = skillSlot;
                }
                return entityState;
            }
            public static EntityState InstantiateBoost(GenericSkill skillSlot, SerializableEntityStateType boost)
            {
                EntityState entityState = EntityStateCatalog.InstantiateState(boost.stateType);
                ISkillState skillState = entityState as ISkillState;
                if (skillState != null)
                {
                    skillState.activatorSkillSlot = skillSlot;
                }
                return entityState;
            }
            public static EntityState InstantiateAirBoost(GenericSkill skillSlot, SerializableEntityStateType boost)
            {
                EntityState entityState = EntityStateCatalog.InstantiateState(boost.stateType);
                ISkillState skillState = entityState as ISkillState;
                if (skillState != null)
                {
                    skillState.activatorSkillSlot = skillSlot;
                }
                if (typeof(EntityStates.Boost).IsAssignableFrom(boost.stateType))
                {
                    ((EntityStates.Boost)entityState).airBoosting = true;
                }
                return entityState;
            }
        }

        public class RequiresFormBoostSkillDef : Forms.SkillDefs.RequiresFormSkillDef, IBoostSkill
        {
            public SerializableEntityStateType boostIdleState { get; set; }
            public SerializableEntityStateType brakeState { get; set; }
            public Color boostHUDColor { get; set; }

            public override EntityState InstantiateNextState([NotNull] GenericSkill skillSlot)
            {
                if (!skillSlot.characterBody || !skillSlot.characterBody.characterMotor) { return base.InstantiateNextState(skillSlot); }
                return BoostSkillDef.DetermineNextBoostState(skillSlot, activationState, boostIdleState);
            }
        }
    }
}
