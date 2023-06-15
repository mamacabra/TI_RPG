using System;
using System.IO;
using Inventory;
using UnityEngine;

namespace Utilities
{
    public static class JsonStorage
    {
        public static void SaveFile(object data, string path)
        {
            try
            {
                string json = JsonUtility.ToJson(data);
                File.WriteAllText(path, json);
            }
            catch (Exception)
            {
                throw new Exception($"[Exception]: Erro ao salvar arquivo JSON: {path}");
            }
        }

        public static InventorySaveData LoadFile(string path)
        {
            try
            {
                string s = File.ReadAllText(path);
                return JsonUtility.FromJson<InventorySaveData>(s);
            }
            catch (Exception)
            {
                throw new Exception($"[Exception]: Erro ao carregar arquivo JSON: {path}");
            }
        }
    }
}
