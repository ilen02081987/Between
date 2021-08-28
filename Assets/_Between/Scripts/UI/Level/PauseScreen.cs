using UnityEngine;
using Between.UI.Base;
using Between.UI.Menu;

namespace Between.UI.Level
{
    public class PauseScreen : UiScreen
    {
        [SerializeField] private LoadGameButton _loadButton;

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

        protected override void PerformOnEnable()
        {
            _loadButton.UpdateVisibility();
        }
    }
}