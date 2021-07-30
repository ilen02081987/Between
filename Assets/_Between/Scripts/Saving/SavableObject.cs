using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Between.Saving
{
    public class SavableObject : MonoBehaviour
    {
        public int Id => _id;

        [SerializeField] private int _id;

#if UNITY_EDITOR
        [MenuItem("SaveSystem/Generate objects ids")]
        public static void GenerateObjectsIds()
        {
            var savableObjects = FindObjectsOfType<SavableObject>();

            for (int i = 0; i < savableObjects.Length; i++)
            {
                savableObjects[i]._id = i;
                EditorUtility.SetDirty(savableObjects[i]);
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
#endif
    }
}