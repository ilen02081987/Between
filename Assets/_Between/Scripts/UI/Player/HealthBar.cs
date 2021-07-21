using Between.UI.Base;

namespace Between.UI
{
    public class HealthBar : UiBar
    {
        private PlayerController _player;
        
        protected override void Run()
        {
            _player = Player.Instance.Controller;
            _player.LivesValueChanged += UpdateValue;
            UpdateValue();
        }

        protected override void PerformOnDestroy()
        {
            _player.LivesValueChanged -= UpdateValue;
        }

        private void UpdateValue()
        {
            UpdateBar(_player.Health / _player.MaxHealth);
        }
    }
}