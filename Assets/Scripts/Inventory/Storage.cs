using System;
using System.Collections.Generic;
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

        public static List<ItemScriptableObject> LoadHeroInventory(int heroId = 0, string filePath = Constants.SaveFile.Inventory)
        {
            try
            {
                InventorySaveData inventorySaveData = JsonStorage.LoadFile<InventorySaveData>(filePath);

                return heroId switch
                {
                    1 => GetItemsFromPath(inventorySaveData.itemsHero1),
                    2 => GetItemsFromPath(inventorySaveData.itemsHero2),
                    _ => GetItemsFromPath(inventorySaveData.itemsHero3)
                };
            }
            catch (Exception)
            {
                return GetItemsFromPath(Constants.InventoryItems.Initial);
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
    }
}
