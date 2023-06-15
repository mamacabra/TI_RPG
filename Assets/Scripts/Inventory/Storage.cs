using System;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using Utilities;

namespace Inventory
{
    public static class Storage
    {
        public static void SaveInventory(string[] items)
        {
            InventorySaveData saveData = new InventorySaveData(items);
            JsonStorage.SaveFile(saveData, Constants.SaveFile.Inventory);
        }

        public static List<ItemScriptableObject> LoadInventory()
        {
            try
            {
                // TODO: JsonStorage.LoadFile<InventorySaveData>(Constants.SaveFile.Inventory);
                InventorySaveData inventorySaveData = JsonStorage.LoadFile(Constants.SaveFile.Inventory);
                return GetItemsFromPath(inventorySaveData.items);
            }
            catch (Exception)
            {
                return GetItemsFromPath(Constants.InventoryItems.Initial);
            }
        }

        private static List<ItemScriptableObject> GetItemsFromPath(string[] paths)
        {
            List<ItemScriptableObject> inventory = new List<ItemScriptableObject>();

            foreach(string path in paths)
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
