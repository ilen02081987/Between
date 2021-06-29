using UnityEngine;
using Between.Damage;
using Between.Interfaces;
using Between.Teams;

namespace Between
{
    public abstract class BaseDamagableObject : MonoBehaviour, IDamagable
    {
        public abstract Team Team { get; }
        public Protection[] Protections { get; }

        public float MaxHealth;
        
        protected float health { get; private set; }

        [SerializeField] private Protection[] _protections;

        protected virtual void Start()
        {
            health = MaxHealth;
        }

        public void ApplyDamage(DamageType type, float damage)
        {
            if (health <= 0)
                return;

            TryDamageProtection(type, ref damage);
            TryDamageHealth(damage);

            if (health <= 0)
                PerformOnDie();
            else
                PerformOnDamage();
        }

        protected abstract void PerformOnDie();
        protected virtual void PerformOnDamage() { }

        private void TryDamageProtection(DamageType type, ref float damage)
        {
            if (_protections == null || _protections.Length == 0)
                return;

            foreach (var protection in _protections)
            {
                if (protection.DamageType == type && protection.Value > 0)
                {
                    protection.Value -= damage;
                    damage = protection.Value < 0 ? -protection.Value : 0f;
                }
            }
        }

        private void TryDamageHealth(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
        }
    }
}