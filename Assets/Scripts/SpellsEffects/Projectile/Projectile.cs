using Between.Interfaces;
using Between.Teams;
using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private Vector3 _velocity;
        private float _damage;
        private Team _team;
        
        private float _blastRadius = 2f;

        #region BEHAVIOUR

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 velocity, float damage, Team team)
        {
            _velocity = velocity;
            _damage = damage;
            _team = team;
        }

        private void Update()
        {
            _rigidbody.velocity = _velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryApplyDamage(collision.gameObject);
            Blast();

            DestroyProjectile();
        }

        #endregion

        #region PRIVATE METHODS

        private void Blast()
        {
            var blastetColliders = Physics.OverlapSphere(transform.position, _blastRadius);

            foreach (var collider in blastetColliders)
                TryApplyDamage(collider.gameObject);
        }

        private void TryApplyDamage(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                if (damagable.Team != _team)
                    damagable.ApplyDamage(_damage);
            }
        }

        private void DestroyProjectile() => Destroy(gameObject);

        #endregion
    }
}