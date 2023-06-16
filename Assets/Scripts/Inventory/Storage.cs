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
            pathList = new string[itemsHero1.Count];
            for (int i = 0; i < itemsHero1.Count; i++)
            {
                pathList[i] = itemsHero1[i].resourcePath;
            }
            pathList = new string[itemsHero2.Count];
            for (int i = 0; i < itemsHero2.Count; i++)
            {
                pathList[i] = itemsHero2[i].resourcePath;
            }
            pathList = new string[itemsHero3.Count];
            for (int i = 0; i < itemsHero3.Count; i++)
            {
                pathList[i] = itemsHero3[i].resourcePath;
            }

            InventorySaveData saveData = new InventorySaveData(pathList);
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
