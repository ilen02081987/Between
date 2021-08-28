using Between.Data;
using Between.UI.Base;
using Between.SceneManagement;

namespace Between.UI.Menu
{
    public class LoadGameButton : UiButton
    {
        public void UpdateVisibility() => Init();

        protected override void Init()
        {
            gameObject.SetActive(DataManager.Instance.HasData);
        }

        private void OnEnable()
        {
            if (!DataManager.Instance.HasData && isInitialized)
                gameObject.SetActive(false);
        }

        protected override void PerformOnClick()
        {
            PauseManager.Play();
            SceneChanger.ChangeScene(gameObject.scene.buildIndex, DataManager.Instance.SavedData.LevelSceneBuildIndex);
        }
    }
}