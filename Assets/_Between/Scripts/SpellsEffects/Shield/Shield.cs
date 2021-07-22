using System.Collections;
using UnityEngine;
using Between.Damage;
using Between.Teams;

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
            SetTrigger();
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

        private void SetTrigger()
        {
            _collider.isTrigger = CheckPoint();
        }

        private bool CheckPoint()
        {
            var colliders = Physics.OverlapSphere(transform.position, 1f);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<ShieldBridgeZone>(out var zone))
                    return false;
            }

            return true;
        }
    }
}