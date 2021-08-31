using UnityEngine;
using Between.UI.Base;
using Accord.MachineLearning;

namespace Between.UI.Menu
{
    public class SelectDifficultyButton : UiButton
    {
        [SerializeField] private GameSettings _settings;
        [SerializeField] private bool _isDefault = false;

        public bool IsSelected => GameSettings.Instance == _settings;

        protected override void Init()
        {
            if (_isDefault && GameSettings.Instance == null)
                _settings.Init();
        }

        protected override void PerformOnClick()
        {
            _settings.Init();
        }
    }
}