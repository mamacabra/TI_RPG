using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Combat;


public class DeathSave
    {
        
        public static class SaveSystem
        {
            public static void SaveDeath(Character character)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                string path = Application.persistentDataPath + "/character.features";
                FileStream stream = new FileStream(path, FileMode.Create);

                DeathSaveData data = new DeathSaveData(character);
            
                formatter.Serialize(stream, data);
                stream.Close();
            }

            public static DeathSaveData LoadCharacterDeath()
            {
                string path = Application.persistentDataPath + "/characterData";
                if (File.Exists(path))
                {
                    Debug.Log("Path " + path);
                    
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream = new FileStream(path, FileMode.Open);

                    DeathSaveData data = formatter.Deserialize(stream) as DeathSaveData;
                
                    stream.Close();
                    return data;
                }

                return null;

            }
        }
    }
