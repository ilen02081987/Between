using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

namespace Between.Saving
{
    public class SavableObject : MonoBehaviour
    {
        public int Id;

#if UNITY_EDITOR
        [MenuItem("SaveSystem/Generate objects ids")]
        public static void GenerateObjectsIds()
        {
            var savableObjects = FindObjectsOfType<SavableObject>(true);

            for (int i = 0; i < savableObjects.Length; i++)
            {
                savableObjects[i].Id = i;
                EditorUtility.SetDirty(savableObjects[i]);
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
#endif
    }
}