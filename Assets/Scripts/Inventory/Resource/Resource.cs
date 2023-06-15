using System.Collections.Generic;
using Combat;
using UnityEngine;

namespace Inventory.Resource
{
    public static class Resource
    {
        // public static List<string> GetPathFromObjects(List<ItemScriptableObject> inventory)
        // {
        //     List<string> resources = new List<string>();
        //
        //     foreach (ItemScriptableObject item in inventory)
        //     {
        //         string path = AssetDatabase.GetAssetPath(item);
        //         resources.Add(path);
        //     }
        //
        //     return resources;
        // }

        public static List<ItemScriptableObject> GetObjectsFromPath(string[] paths)
        {
            List<ItemScriptableObject> inventory = new List<ItemScriptableObject>();

            foreach(string path in paths)
            {
                ItemScriptableObject item = Resources.Load<ItemScriptableObject>(path);
                if (item is not null) inventory.Add(item);
            }

            return inventory;
        }
    }
}
