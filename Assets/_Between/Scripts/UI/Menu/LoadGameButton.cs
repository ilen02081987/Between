using Between.Data;
using Between.UI.Base;
using Between.SceneManagement;

namespace Between.UI.Menu
{
    public class LoadGameButton : UiButton
    {
        protected override void Init()
        {
            if (!DataManager.Instance.HasData)
                gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (!DataManager.Instance.HasData && isInitialized)
                gameObject.SetActive(false);
        }

        protected override void PerformOnClick()
        {
            SceneChanger.ChangeScene(gameObject.scene.buildIndex, DataManager.Instance.SavedData.LevelSceneBuildIndex);
        }
    }
}