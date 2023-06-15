using System.Collections.Generic;
using Combat;
using TMPro;
using UnityEngine;

namespace Inventory.Resource
{
    public class ResourceSample : MonoBehaviour
    {
        public TMP_Text textMesh;

        public List<string> paths;
        public List<ItemScriptableObject> inventory;
        public List<ItemScriptableObject> inventoryLoaded;

        private void OnMouseUp()
        {
            // paths = Parser.GetPathFromObjects(inventory);

            paths = new List<string>
            {
                "Items/SwordTiny/Item_SwordTiny",
                "Items/SwordBig/Item_SwordBig",
            };

            inventoryLoaded = Resource.GetObjectsFromPath(paths);
            textMesh.text = "";

            foreach (var item in inventoryLoaded)
            {
                textMesh.text += item.name + "\n";
                Debug.Log(item.name);
            }
        }
    }
}
