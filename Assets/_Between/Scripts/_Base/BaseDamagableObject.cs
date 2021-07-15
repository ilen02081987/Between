using UnityEngine;
using Between.Damage;
using Between.Interfaces;
using Between.Teams;
using System;

namespace Between
{
    public abstract class BaseDamagableObject : MonoBehaviour, IDamagable
    {
        public event Action OnDamage;
        public event Action OnDie;

        public abstract Team Team { get; }

        public float MaxHealth;
        public float Health { get; protected set; }

        [SerializeField] private Protection[] _protections;

        public void InitDamagableObject()
        {
            Health = MaxHealth;
        }

        public void ApplyDamage(DamageItem damage)
        {
            if (Health <= 0)
                return;

            TryDamageProtection(damage.Type, ref damage.Value);
            TryDamageHealth(damage.Value);

            if (Health > 0)
            {
                OnDamage?.Invoke();
                PerformOnDamage();
            }
            else
            {
                OnDie?.Invoke();
                PerformOnDie();
            }
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
            Health = Mathf.Max(Health - damage, 0f);
        }
    }
}