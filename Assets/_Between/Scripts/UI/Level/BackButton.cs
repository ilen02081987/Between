using Between.UI.Base;

namespace Between.UI.Level
{
    public class BackButton : UiButton
    {
        protected override void PerformOnClick()
        {
            PauseManager.Play();
        }
    }
}