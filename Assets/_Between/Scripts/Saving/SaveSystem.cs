using System.IO;
using UnityEngine;
using Between.Data;

namespace Between.Saving
{
    public class SaveSystem
    {
        public static string FilePath => Path.Combine(Folder, "PlayerData.json");
        public static string Folder => Application.persistentDataPath;
        public static bool CanLoad => File.Exists(FilePath);

        public static void Save(PlayerData data)
        {
            var jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(FilePath, jsonData);
        }

        public static PlayerData Load()
        {
            if (!CanLoad)
                throw new System.Exception($"There is no PlayerData at {FilePath}! Can't load.");

            var jsonData = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }

        public static void ClearSave()
        {
            if (CanLoad)
                File.Delete(FilePath);
        }
    }
}