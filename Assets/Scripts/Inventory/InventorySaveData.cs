using System;

namespace Inventory
{
    [Serializable]
    public class InventorySaveData
    {
        public string[] items;
        public string[] itemsHero1;
        public string[] itemsHero2;
        public string[] itemsHero3;

        public InventorySaveData(string[] items)
        {
            this.items = items;
        }
    }
}
