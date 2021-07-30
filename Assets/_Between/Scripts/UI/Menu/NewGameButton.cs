using Between.Data;
using Between.SceneManagement;
using Between.UI.Base;
using UnityEngine.SceneManagement;

namespace Between.UI.Menu
{
    public class NewGameButton : UiButton
    {
        protected override void PerformOnClick()
        {
            DataManager.Instance.ClearSave();
            SceneChanger.ChangeScene(1, 2);
        }
    }
}