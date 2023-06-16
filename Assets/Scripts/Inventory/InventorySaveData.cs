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

        public InventorySaveData(string[] items, string[] itemsHero1, string[] itemsHero2, string[] itemsHero3)
        {
            this.items = items;
            this.itemsHero1 = itemsHero1;
            this.itemsHero2 = itemsHero2;
            this.itemsHero3 = itemsHero3;
        }
        public InventorySaveData(string[] items)
        {
            this.items = items;
        }
    }
}
