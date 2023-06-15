using System;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using Utilities;

namespace Inventory.Samples
{
    public class InventoryLoadSample : MonoBehaviour
    {
        [SerializeField] private List<ItemScriptableObject> inventory;

        private void OnMouseUp()
        {
            try
            {
                // TODO: JsonStorage.LoadFile<InventorySaveData>(Constants.SaveFile.Inventory);
                InventorySaveData inventorySaveData = JsonStorage.LoadFile(Constants.SaveFile.Inventory);
                inventory = Resource.GetObjectsFromPath(inventorySaveData.items);
            }
            catch (Exception)
            {
                inventory = Resource.GetObjectsFromPath(Constants.InventoryItems.Initial);
            }
        }
    }
}
