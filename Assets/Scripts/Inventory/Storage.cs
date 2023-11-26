using System;
using System.Collections.Generic;
using System.Linq;
using Combat;
using UnityEngine;
using Utilities;

namespace Inventory
{
    public static class Storage
    {
        public static void SaveInventory(string[] pathList, string filePath = Constants.SaveFile.Inventory)
        {
            InventorySaveData saveData = new InventorySaveData(pathList);
            JsonStorage.SaveFile(saveData, filePath);
        }
        public static void DeleteInventory(string filePath = Constants.SaveFile.Inventory)
        {
            JsonStorage.DeleteFile(filePath);
        }

        public static void SaveInventory(List<ItemScriptableObject> items, List<ItemScriptableObject> itemsHero1, List<ItemScriptableObject> itemsHero2, List<ItemScriptableObject> itemsHero3, string filePath = Constants.SaveFile.Inventory)
        {
            string[] pathList = new string[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                pathList[i] = items[i].resourcePath;
            }
            string[] pathList1 = new string[itemsHero1.Count];
            for (int i = 0; i < itemsHero1.Count; i++)
            {
                pathList1[i] = itemsHero1[i].resourcePath;
            }
            string[] pathList2 = new string[itemsHero2.Count];
            for (int i = 0; i < itemsHero2.Count; i++)
            {
                pathList2[i] = itemsHero2[i].resourcePath;
            }
            string[] pathList3 = new string[itemsHero3.Count];
            for (int i = 0; i < itemsHero3.Count; i++)
            {
                pathList3[i] = itemsHero3[i].resourcePath;
            }

            InventorySaveData saveData = new InventorySaveData(pathList, pathList1, pathList2, pathList3);
            JsonStorage.SaveFile(saveData, filePath);
        }

        public static List<ItemScriptableObject> LoadInventory(string filePath = Constants.SaveFile.Inventory)
        {
            try
            {
                InventorySaveData inventorySaveData = JsonStorage.LoadFile<InventorySaveData>(filePath);
                return GetItemsFromPath(inventorySaveData.items);
            }
            catch (Exception)
            {
                return GetItemsFromPath(Constants.InventoryItems.Initial);
            }
        }

        public static bool TryLoadInventory(out List<ItemScriptableObject> inventory, string filePath = Constants.SaveFile.Inventory)
        {
            try
            {
                InventorySaveData inventorySaveData = JsonStorage.LoadFile<InventorySaveData>(filePath);
                inventory = GetItemsFromPath(inventorySaveData.items);
                return true;
            }
            catch (Exception)
            {
                inventory = new();
                if(TryGetItemsFromPath(Constants.InventoryItems.InitialItems, out List<ItemScriptableObject> _inventory)) {inventory = _inventory;}
                return false;
            }
        }

        public static bool TryLoadAllItems(out List<ItemScriptableObject> allItems)
        {
            bool success = false;
            allItems = new List<ItemScriptableObject>();
            if(TryGetItemsFromPath(Constants.InventoryItems.InitialItems, out List<ItemScriptableObject> initialItems)) {allItems = initialItems; success = true;}
            if(TryGetItemsFromPath(Constants.InventoryItems.HeroesItems, out List<ItemScriptableObject> heroesItems)) {allItems.AddRange(heroesItems); success = true; }
            return success;
        }

        public static List<ItemScriptableObject> LoadHeroInventory(int heroId = 0, string filePath = Constants.SaveFile.Inventory)
        {
            try
            {
                InventorySaveData inventorySaveData = JsonStorage.LoadFile<InventorySaveData>(filePath);

                return heroId switch
                {
                    0 => GetItemsFromPath(inventorySaveData.itemsHero1),
                    1 => GetItemsFromPath(inventorySaveData.itemsHero2),
                    _ => GetItemsFromPath(inventorySaveData.itemsHero3)
                };
            }
            catch (Exception)
            {
                return GetItemsFromPath(Constants.InventoryItems.Initial);
            }
        }
        public static bool TryLoadHeroInventory(out List<ItemScriptableObject> items, int heroId = 0, string filePath = Constants.SaveFile.Inventory)
        {
            try
            {
                InventorySaveData inventorySaveData = JsonStorage.LoadFile<InventorySaveData>(filePath);
                items = new List<ItemScriptableObject>();
                switch(heroId)
                {
                    case 0: items = GetItemsFromPath(inventorySaveData.itemsHero1); break;
                    case 1: items = GetItemsFromPath(inventorySaveData.itemsHero2); break;
                    case 2: items = GetItemsFromPath(inventorySaveData.itemsHero3); break;
                    default: return false;
                };
                return true;
            }
            catch (Exception)
            {
                items = new List<ItemScriptableObject>();
                return false;
            }
        }

        private static List<ItemScriptableObject> GetItemsFromPath(string[] pathList)
        {
            List<ItemScriptableObject> inventory = new List<ItemScriptableObject>();

            foreach (string path in pathList)
            {
                ItemScriptableObject item = Resources.Load<ItemScriptableObject>(path);

                if (item is null) continue;

                item.resourcePath = path;
                inventory.Add(item);
            }

            return inventory;
        }

        private static bool TryGetItemsFromPath(string path, out List<ItemScriptableObject> inventory)
        {
            bool success = false;
            inventory = Resources.LoadAll<ItemScriptableObject>(path).ToList();
            if(inventory.Count > 0) success = true;
            return success;
        }
    }
}
