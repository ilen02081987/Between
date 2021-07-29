using UnityEngine.SceneManagement;
using Between.Data;
using Between.UI.Base;

namespace Between.UI.Menu
{
    public class LoadGameButton : UiButton
    {
        protected override void Init()
        {
            if (DataManager.Instance.SavedData == null)
                gameObject.SetActive(false);
        }

        protected override void PerformOnClick()
        {
            SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(DataManager.Instance.SavedData.LevelSceneBuildIndex, LoadSceneMode.Additive);
        }
    }
}