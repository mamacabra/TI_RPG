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

        public static void SaveInventory(List<ItemScriptableObject> items, string filePath = Constants.SaveFile.Inventory)
        {
            string[] pathList = new string[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                pathList[i] = items[i].resourcePath;
            }

            InventorySaveData saveData = new InventorySaveData(pathList);
            JsonStorage.SaveFile(saveData, filePath);
        }

        public static List<ItemScriptableObject> LoadInventory(string filePath = Constants.SaveFile.Inventory)
        {
            try
            {
                // TODO: JsonStorage.LoadFile<InventorySaveData>(Constants.SaveFile.Inventory);
                InventorySaveData inventorySaveData = JsonStorage.LoadFile(filePath);
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

            foreach(string path in pathList)
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
