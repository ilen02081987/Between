using UnityEngine;
using Between.UI.Base;

namespace Between.UI.Menu
{
    public class SelectDifficultyButton : UiButton
    {
        [SerializeField] private GameSettings _settings;
        [SerializeField] private bool _isDefault = false;

        protected override void Init()
        {
            if (_isDefault)
                _settings.InitSettings();
        }

        protected override void PerformOnClick()
        {
            _settings.InitSettings();
        }
    }
}