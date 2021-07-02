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
        public float MaxHealth { get; protected set; }
        public float Health { get; private set; }

        [SerializeField] private Protection[] _protections;

        protected virtual void Start()
        {
            Health = MaxHealth;
        }

        public void ApplyDamage(DamageType type, float damage)
        {
            if (Health <= 0)
                return;

            TryDamageProtection(type, ref damage);
            TryDamageHealth(damage);

            if (Health <= 0)
                PerformOnDie();
            else
                PerformOnDamage();
        }

        protected abstract void PerformOnDie();
        protected virtual void PerformOnDamage() { }

        protected void InvokeDamageEvent() => OnDamage?.Invoke();
        protected void InvokeDieEvent() => OnDie?.Invoke();

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