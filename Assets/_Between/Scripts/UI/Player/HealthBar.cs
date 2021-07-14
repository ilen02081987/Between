using UnityEngine;
using UnityEngine.UI;

namespace Between.UI
{
    public class HealthBar : UiBar
    {
        private PlayerController _player;
        
        protected override void Run()
        {
            _player = Player.Instance.Controller;
            _player.OnDamage += UpdateValue;
            UpdateValue();
        }

        protected override void PerformOnDestroy()
        {
            _player.OnDamage -= UpdateValue;
        }

        private void UpdateValue()
        {
            UpdateBar(_player.Health / _player.MaxHealth);
        }
    }
}