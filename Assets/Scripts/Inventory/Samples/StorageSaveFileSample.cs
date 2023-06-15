using Constants;
using UnityEngine;

namespace Inventory.Samples
{
    public class StorageSaveFileSample : MonoBehaviour
    {
        [SerializeField] private string[] inventory = InventoryItems.Initial;

        private void OnMouseUp()
        {
            InventorySaveData saveData = new InventorySaveData(inventory);
            Utils.JsonStorage.SaveFile(saveData, SaveFile.Inventory);
        }
    }
}
