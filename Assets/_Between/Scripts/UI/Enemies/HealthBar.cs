using UnityEngine;
using UnityEngine.UI;
using Between.Enemies;

namespace Between.UI.Enemies
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        private BaseEnemy _enemy;
        private Transform _anchor;
        private Slider _slider;

        public void AttachTo(BaseEnemy enemy, Transform anchor)
        {
            _enemy = enemy;
            _anchor = anchor;

            if (!GameSettings.Instance.EnemyHealthUIEnabled)
            {
                Destroy(gameObject);
            }
            else
            {
                _slider = GetComponent<Slider>();
                _enemy.LivesValueChanged += UpdateValue;
                _enemy.OnDie += HideSlider;

                _slider.value = 1f;
            }
        }

        private void Update()
        {
            if (this != null)
                transform.position = GameCamera.WorldToScreenPoint(_anchor.position);
        }

        private void HideSlider()
        {
            _enemy.LivesValueChanged -= UpdateValue;
            _enemy.OnDie -= HideSlider;

            Destroy(gameObject);
        }

        private void UpdateValue()
        {
            _slider.value = _enemy.Health / _enemy.MaxHealth;
        }
    }
}