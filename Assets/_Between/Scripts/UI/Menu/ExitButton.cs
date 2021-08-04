using Between.UI.Base;
using UnityEngine;

namespace Between.UI.Menu
{
    public class ExitButton : UiButton
    {
        protected override void PerformOnClick()
        {
            Application.Quit();
        }
    }
}