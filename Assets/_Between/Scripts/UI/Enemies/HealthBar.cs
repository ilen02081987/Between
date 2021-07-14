using Between.Enemies;
using UnityEngine;

namespace Between.UI.Enemies
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private BaseEnemy _enemy;

        private void Start()
        {
            if (!GameSettings.Instance.EnemyHealthUIEnabled)
            {
                Destroy(gameObject);
            }
            else
            {
                _enemy.OnDamage += UpdateValue;
            }
        }

        private void UpdateValue()
        {
            
        }
    }
}