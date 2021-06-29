#if UNITY_EDITOR

using Between.Saving;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorMethods : MonoBehaviour
{
    [MenuItem("SaveSystem/Show save file folder")]
    private static void ShowSaveFileFolder()
    {
        if (!Directory.Exists(SaveSystem.Folder))
            Directory.CreateDirectory(SaveSystem.Folder);

        Process.Start(new ProcessStartInfo()
        {
            FileName = SaveSystem.Folder,
            UseShellExecute = true,
            Verb = "open"
        });
    }

    [MenuItem("SaveSystem/Delete save file")]
    private static void DeleteSave()
    {
        if (SaveSystem.CanLoad())
            File.Delete(SaveSystem.FilePath);
    }
}

#endif
