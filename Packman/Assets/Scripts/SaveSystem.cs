using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game {
    public static class SaveSystem {

        public static void SaveData(object data, string fileName) {
            var path = Application.persistentDataPath + $"/{fileName}";
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Create);

            if(data == null)
                Debug.Log($"input data null");
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static object LoadFile(string fileName) {            
            var path = Application.persistentDataPath + $"/{fileName}";

            try {
                if (File.Exists((path))) {
                    var formatter = new BinaryFormatter();

                    var stream = new FileStream(path, FileMode.Open);
                    if (stream.Length == 0) {
                        stream.Close();
                        return null;
                    }

                    stream.Position = 0;
                    var data = formatter.Deserialize(stream);
                    stream.Close();
                    return data;
                }
                else {
                    var stream = new FileStream(path, FileMode.CreateNew);
                    stream.Close();
                    return null;
                }
            }
            catch (Exception e){
                Debug.LogError(e.Message);
                Debug.Log(path);
            }

            return null;
        }
    }
}