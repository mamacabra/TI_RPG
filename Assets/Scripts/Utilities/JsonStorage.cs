using System.IO;
using UnityEngine;

namespace Utils
{
    public static class JsonStorage
    {
        public static void SaveFile(object data, string path)
        {
            try
            {
                string contents = JsonUtility.ToJson(data);
                File.WriteAllText(path, contents);
                Debug.Log("[Success]: Arquivo JSON salvo com sucesso em: " + path);
            }
            catch (System.Exception ex)
            {
                Debug.Log($"[Exception]: Erro ao salvar arquivo JSON: {ex.Message}");
            }
        }
    }
}
