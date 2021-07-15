using Between.Mana;
using Between.UI.Base;

namespace Between.UI
{
    public class ManaBar : UiBar
    {
        private ManaHolder _playerMana;

        protected override void Run()
        {
            _playerMana = Between.Player.Instance.Mana;
            _playerMana.OnValueChanged += UpdateValue;
            UpdateValue();
        }

        protected override void PerformOnDestroy()
        {
            _playerMana.OnValueChanged -= UpdateValue;
        }

        private void UpdateValue()
        {
            UpdateBar(_playerMana.Value / _playerMana.MaxValue);
        }
    }
}