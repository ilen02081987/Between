using Between.Damage;
using Between.Interfaces;
using Between.Teams;
using System;
using System.Collections;
using UnityEngine;

namespace Between.SpellsEffects.ShieldSpell
{
    public class Shield : BaseDamagableObject
    {
        public float Size => transform.localScale.y;
        public float LifeTime => _lifeTime;
        public override Team Team => _team;

        [SerializeField] private Team _team;
        [SerializeField] private float _lifeTime = 3f;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            StartCoroutine(WaitToDestroy());
            InitDamagableObject();
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);

            if (this != null && gameObject != null)
                DestroyShield();
        }

        public void ApplyDamage(DamageItem damage, float attackRadius)
        {
            var blastetColliders = Physics.OverlapSphere(transform.position, attackRadius);

            foreach (var collider in blastetColliders)
            {
                if (collider.TryGetComponent<Shield>(out var damagable))
                    damagable.ApplyDamage(damage);
            }
        }

        protected override void PerformOnDie()
        {
            DestroyShield();
        }

        private void DestroyShield()
        {
            StopCoroutine(WaitToDestroy());
            Destroy(gameObject);
        }

        internal void SetTrigger(bool isTrigger)
        {
            _collider.isTrigger = isTrigger;
        }
    }
}