using System.Collections.Generic;
using Combat;
using Constants;
using TMPro;
using UnityEngine;

namespace Inventory.Samples
{
    public class ResourceSample : MonoBehaviour
    {
        [SerializeField] private TMP_Text textMesh;
        [SerializeField] private List<ItemScriptableObject> inventory;

        private void OnMouseUp()
        {
            string[] paths = InventoryItems.Initial;
            inventory = Resource.GetObjectsFromPath(paths);

            textMesh.text = "";
            foreach (var item in inventory)
            {
                Debug.Log(item.name);
                textMesh.text += item.name + "\n";
            }
        }
    }
}
