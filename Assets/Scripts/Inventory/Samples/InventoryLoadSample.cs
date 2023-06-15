using System.Collections.Generic;
using Combat;
using UnityEngine;

namespace Inventory.Samples
{
    public class InventoryLoadSample : MonoBehaviour
    {
        [SerializeField] private List<ItemScriptableObject> inventory;

        private void OnMouseUp()
        {
            inventory = Storage.LoadInventory();
        }
    }
}
