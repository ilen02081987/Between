using Between.Data;
using Between.UI.Base;
using UnityEngine.SceneManagement;

namespace Between.UI.Menu
{
    public class NewGameButton : UiButton
    {
        protected override void PerformOnClick()
        {
            DataManager.Instance.ClearSave();

            SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}