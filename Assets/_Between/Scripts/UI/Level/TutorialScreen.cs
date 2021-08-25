using UnityEngine;
using Between.UI.Base;
using Between.Data;

namespace Between.UI.Level
{
    public class TutorialScreen : UiScreen
    {
        public void Show() => Enable();

        private void Start()
        {
            if (!DataManager.Instance.HasData)
                Enable();
        }

        private void Update()
        {
            if (Input.anyKeyDown)
                Disable();
        }
    }
}