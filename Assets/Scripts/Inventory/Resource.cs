using System.Collections.Generic;
using Combat;
using UnityEngine;

namespace Inventory
{
    public static class Resource
    {
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
