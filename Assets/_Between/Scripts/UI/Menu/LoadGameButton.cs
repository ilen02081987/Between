using Between.Data;
using Between.UI.Base;
using Between.SceneManagement;

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
            SceneChanger.ChangeScene(1, DataManager.Instance.SavedData.LevelSceneBuildIndex);
        }
    }
}