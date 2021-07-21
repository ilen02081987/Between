using Between.Enemies;
using Between.UI.Base;
using UnityEngine;

namespace Between.UI.Enemies
{
    [RequireComponent(typeof(PlaneSlider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private BaseEnemy _enemy;

        private PlaneSlider _slider;

        private void Start()
        {
            if (!GameSettings.Instance.EnemyHealthUIEnabled)
            {
                Destroy(gameObject);
            }
            else
            {
                _slider = GetComponent<PlaneSlider>();
                _enemy.LivesValueChanged += UpdateValue;
                _enemy.OnDie += HideSlider;

                _slider.Value = 1f;
            }
        }

        private void HideSlider()
        {
            _enemy.LivesValueChanged -= UpdateValue;
            _enemy.OnDie -= HideSlider;
            _slider.Disable();
        }

        private void UpdateValue()
        {
            _slider.Value = _enemy.Health / _enemy.MaxHealth;
        }
    }
}