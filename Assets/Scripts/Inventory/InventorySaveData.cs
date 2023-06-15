using System;

namespace Inventory
{
    [Serializable]
    public class InventorySaveData
    {
        public string[] inventory;

        public InventorySaveData(string[] inventory)
        {
            this.inventory = inventory;
        }
    }
}
