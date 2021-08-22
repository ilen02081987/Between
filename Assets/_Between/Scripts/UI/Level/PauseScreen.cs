using Between.UI.Base;

namespace Between.UI.Level
{
    public class PauseScreen : UiScreen
    {
        private void Start()
        {
            PauseManager.OnPause += Enable;
            PauseManager.OnPlay += Disable;
        }

        private void OnDestroy()
        {
            PauseManager.OnPause -= Enable;
            PauseManager.OnPlay -= Disable;
        }
    }
}