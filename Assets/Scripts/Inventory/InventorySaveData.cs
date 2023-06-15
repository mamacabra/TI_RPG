using System;

namespace Inventory
{
    [Serializable]
    public class InventorySaveData
    {
        public string[] items;

        public InventorySaveData(string[] items)
        {
            this.items = items;
        }
    }
}
