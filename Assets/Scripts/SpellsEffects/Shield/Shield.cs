using Between.Damage;
using Between.Interfaces;
using Between.Teams;
using System.Collections;
using UnityEngine;

namespace Between.SpellsEffects.ShieldSpell
{
    public class Shield : BaseDamagableObject
    {
        public float Size => transform.localScale.y;

        public override Team Team => Team.Player;

        private float _lifeTime = 3f;

        private void Start()
        {
            _lifeTime = GameSettings.Instance.ShieldLifeTime;
            health = GameSettings.Instance.ShieldHealth;

            StartCoroutine(WaitToDestroy());
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);

            if (this != null && gameObject != null)
                DestroyShield();
        }

        public void ApplyDamage(DamageType type, float damage, float attackRadius)
        {
            var blastetColliders = Physics.OverlapSphere(transform.position, attackRadius);

            foreach (var collider in blastetColliders)
            {
                if (collider.TryGetComponent<Shield>(out var damagable))
                    damagable.ApplyDamage(type, damage);
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
    }
}