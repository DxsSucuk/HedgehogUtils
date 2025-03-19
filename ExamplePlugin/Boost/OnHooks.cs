using RoR2;
using System;
using UnityEngine;
using R2API;
using UnityEngine.Networking;
using HedgehogUtils.Internal;
using R2API.Networking;
using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using LookingGlass.ItemStatsNameSpace;
using LookingGlass.LookingGlassLanguage;
using System.Linq;
using HedgehogUtils.Boost.EntityStates;

namespace HedgehogUtils.Boost
{
    public static class OnHooks
    {
        public static void Initialize()
        {
            On.RoR2.GenericSkill.CanApplyAmmoPack += CanApplyAmmoPackToBoost;
            On.RoR2.GenericSkill.ApplyAmmoPack += ApplyAmmoPackToBoost;
        }

        private static bool CanApplyAmmoPackToBoost(On.RoR2.GenericSkill.orig_CanApplyAmmoPack orig, GenericSkill self)
        {
            if (typeof(EntityStates.Boost).IsAssignableFrom(self.activationState.stateType))
            {
                BoostLogic boost = self.characterBody.GetComponent<BoostLogic>();
                if (boost)
                {
                    return boost.boostMeter < boost.maxBoostMeter;
                }
            }
            return orig(self);
        }
        private static void ApplyAmmoPackToBoost(On.RoR2.GenericSkill.orig_ApplyAmmoPack orig, GenericSkill self)
        {
            orig(self);
            if (typeof(EntityStates.Boost).IsAssignableFrom(self.activationState.stateType))
            {
                BoostLogic boost = self.characterBody.GetComponent<BoostLogic>();
                if (boost)
                {
                    boost.AddBoost(BoostLogic.boostRegenPerBandolier);
                }
            }
        }
    }
}