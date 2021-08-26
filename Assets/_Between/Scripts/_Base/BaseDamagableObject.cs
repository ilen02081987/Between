using UnityEngine;
using Between.Damage;
using Between.Interfaces;
using Between.Teams;
using System;
using System.IO;

namespace Between
{
    public abstract class BaseDamagableObject : MonoBehaviour, IDamagable
    {
        private static GameObject _healVfxPrefab;
        private static string _prefabName = Path.Combine(ResourcesFoldersNames.VFX, "Vfx_Heal_IdentifyFinish");

        public event Action LivesValueChanged;
        public event Action OnDie;

        public abstract Team Team { get; }

        public float MaxHealth;
        public float Health { get; protected set; }
        public bool Immortal = false;

        public Vector3 Position => transform.position;

        [SerializeField] private Protection[] _protections;

        public void SetMaxHealth(float to)
        {
            MaxHealth = to;
        }

        public void InitDamagableObject()
        {
            Health = MaxHealth;
        }

        public void ApplyDamage(DamageItem damage)
        {
            if (Health <= 0 || Immortal) 
                return;

            TryDamageProtection(damage.Type, ref damage.Value);
            TryDamageHealth(damage.Value);

            LivesValueChanged?.Invoke();

            if (Health > 0)
            {
                PerformOnDamage();
            }
            else
            {
                OnDie?.Invoke();
                PerformOnDie();
            }
        }

        public void Heal(float value)
        {
            Health = Mathf.Min(Health + value, MaxHealth);
            LivesValueChanged?.Invoke();

            CreateHealVfx();
        }

        private void CreateHealVfx()
        {
            if (_healVfxPrefab == null)
                _healVfxPrefab = Resources.Load(_prefabName) as GameObject;
            
            Instantiate(_healVfxPrefab, Position, Quaternion.identity);
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