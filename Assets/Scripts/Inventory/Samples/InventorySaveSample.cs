using Constants;
using UnityEngine;
using Utilities;

namespace Inventory.Samples
{
    public class InventorySaveSample : MonoBehaviour
    {
        [SerializeField] private string[] inventory = InventoryItems.Initial;

        private void OnMouseUp()
        {
            InventorySaveData saveData = new InventorySaveData(inventory);
            JsonStorage.SaveFile(saveData, SaveFile.Inventory);
        }
    }
}
