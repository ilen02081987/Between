using System.IO;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
using System.Diagnostics;
#endif

namespace Between.Saving
{
    public class SaveSystem
    {
        public static string FilePath => Path.Combine(Folder, "PlayerData.json");
        public static string Folder => Application.persistentDataPath;

        public static void Save()
        {
            if (Player.Instance == null)
                throw new System.Exception($"There is no PlayerData! Can't save.");

            var jsonData = JsonUtility.ToJson(Player.Instance.Data);
            File.WriteAllText(FilePath, jsonData);
        }

        public static PlayerData Load()
        {
            if (!CanLoad())
                throw new System.Exception($"There is no PlayerData at {FilePath}! Can't load.");

            var jsonData = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }

        public static bool CanLoad() => File.Exists(FilePath);

#if UNITY_EDITOR



#endif
    }
}