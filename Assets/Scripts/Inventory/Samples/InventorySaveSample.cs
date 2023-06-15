using Constants;
using UnityEngine;

namespace Inventory.Samples
{
    public class InventorySaveSample : MonoBehaviour
    {
        [SerializeField] private string[] items = InventoryItems.Initial;

        private void OnMouseUp()
        {
            Storage.SaveInventory(items);
        }
    }
}
