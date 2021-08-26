using UnityEngine;
using Between.UI.Base;
using Between.Data;

namespace Between.UI.Level
{
    public class TutorialScreen : UiScreen
    {
        private bool _exitButtonPressed => Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return);

        public void Show() => Enable();

        private void Start()
        {
            if (!DataManager.Instance.HasData)
                Enable();
        }

        private void Update()
        {
            if (_exitButtonPressed && isEnabled)
                Disable();
        }

        protected override void PerformOnEnable() => PauseManager.Pause();
        protected override void PerformOnDisable() => PauseManager.Play();
    }
}