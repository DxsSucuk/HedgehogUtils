using System.Collections.Generic;
using System;
using HedgehogUtils.Forms.SuperForm.EntityStates;
using HedgehogUtils.Boost.EntityStates;

namespace HedgehogUtils
{
    public static class States
    {
        internal static void RegisterStates()
        {
            Internal.Content.AddEntityState(typeof(InteractablePurchased));
            Internal.Content.AddEntityState(typeof(SuperSonic));
            Internal.Content.AddEntityState(typeof(SuperSonicTransformation));
            Internal.Content.AddEntityState(typeof(Launch.Launched));

            Internal.Content.AddEntityState(typeof(Brake));
        }
    }
}