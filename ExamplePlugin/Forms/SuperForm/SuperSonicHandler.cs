using System;
using System.Collections.Generic;
using System.Text;

namespace HedgehogUtils.Forms.SuperForm
{
    public class SuperSonicHandler : FormHandler
    {
        public static List<ChaosEmeraldInteractable.EmeraldColor> available;

        public void FilterOwnedEmeralds()
        {
            available = new List<ChaosEmeraldInteractable.EmeraldColor>(new ChaosEmeraldInteractable.EmeraldColor[]
            {ChaosEmeraldInteractable.EmeraldColor.Yellow, ChaosEmeraldInteractable.EmeraldColor.Blue, ChaosEmeraldInteractable.EmeraldColor.Red,
                ChaosEmeraldInteractable.EmeraldColor.Gray, ChaosEmeraldInteractable.EmeraldColor.Green, ChaosEmeraldInteractable.EmeraldColor.Cyan, ChaosEmeraldInteractable.EmeraldColor.Purple });

            if (itemTracker.GetType().IsAssignableFrom(typeof(SyncedItemTracker)))
            {
                if (!((SyncedItemTracker)itemTracker).missingItems.Contains(Items.yellowEmerald.itemIndex)) { available.Remove(ChaosEmeraldInteractable.EmeraldColor.Yellow); }
                if (!((SyncedItemTracker)itemTracker).missingItems.Contains(Items.blueEmerald.itemIndex)) { available.Remove(ChaosEmeraldInteractable.EmeraldColor.Blue); }
                if (!((SyncedItemTracker)itemTracker).missingItems.Contains(Items.redEmerald.itemIndex)) { available.Remove(ChaosEmeraldInteractable.EmeraldColor.Red); }
                if (!((SyncedItemTracker)itemTracker).missingItems.Contains(Items.grayEmerald.itemIndex)) { available.Remove(ChaosEmeraldInteractable.EmeraldColor.Gray); }
                if (!((SyncedItemTracker)itemTracker).missingItems.Contains(Items.greenEmerald.itemIndex)) { available.Remove(ChaosEmeraldInteractable.EmeraldColor.Green); }
                if (!((SyncedItemTracker)itemTracker).missingItems.Contains(Items.cyanEmerald.itemIndex)) { available.Remove(ChaosEmeraldInteractable.EmeraldColor.Cyan); }
                if (!((SyncedItemTracker)itemTracker).missingItems.Contains(Items.purpleEmerald.itemIndex)) { available.Remove(ChaosEmeraldInteractable.EmeraldColor.Purple); }
            }
        }
    }
}
