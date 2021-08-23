#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;

namespace Between.Editor
{
    public static class DefaultSceneSwitcher
    {
        [MenuItem("Scenes/Switch to default")]
        public static void SwitchToDefault()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/_Between/Scenes/Preview/App/App.unity");
        }

        [MenuItem("Scenes/Switch to level")]
        public static void SwitchToLevel()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/_Between/Scenes/Preview/Level 0 draft/Level 0.unity");
        }

        [MenuItem("Scenes/Switch to menu")]
        public static void SwitchSToMenu()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/_Between/Scenes/Preview/MainMenu/MainMenu.unity");
        }
    }
}

#endif