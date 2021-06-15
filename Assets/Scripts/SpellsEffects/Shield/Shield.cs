using Between.Interfaces;
using Between.Teams;
using System.Collections;
using UnityEngine;

namespace Between.SpellsEffects.ShieldSpell
{
    public class Shield : MonoBehaviour, IDamagable
    {
        public float Size => transform.localScale.y;
        public Team Team { get; set; } = Team.Player;

        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private float _health = 10;

        private void Start()
        {
            StartCoroutine(WaitToDestroy());
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);

            if (this != null && gameObject != null)
                DestroyShield();
        }

        public void ApplyDamage(float damage, float attackRadius)
        {
            var blastetColliders = Physics.OverlapSphere(transform.position, attackRadius);

            foreach (var collider in blastetColliders)
            {
                if (collider.TryGetComponent<IDamagable>(out var damagable))
                    damagable.ApplyDamage(damage);
            }
        }

        public void ApplyDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
                DestroyShield();
        }

        private void DestroyShield()
        {
            StopCoroutine(WaitToDestroy());
            Destroy(gameObject);
        }

        private void Blast()
        {


            
        }
    }
}