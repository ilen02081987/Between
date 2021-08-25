using UnityEngine;
using Between.UI.Base;
using Between.UI.Level;

public class TutorialButton : UiButton
{
    [SerializeField] private TutorialScreen _screen;

    protected override void PerformOnClick()
    {
        _screen.Show();
    }
}
