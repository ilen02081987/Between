using Between.Interfaces;
using Between.Teams;
using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private ProjectileData _projectileData;
        private Vector3 direction;

        private Transform _transform;

        private bool _hasCollide = false;

        #region BEHAVIOUR

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(ProjectileData projectileData, Vector3 velocity)
        {
            _projectileData = projectileData;
            direction = velocity;
        }

        private void Update()
        {
            _rigidbody.velocity = direction * _projectileData.Speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_hasCollide)
            {
                _hasCollide = true;

                Blast();
                DestroyProjectile();
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void Blast()
        {
            var blastetColliders = Physics.OverlapSphere(transform.position, _projectileData.BlastRadius);

            foreach (var collider in blastetColliders)
                TryApplyDamage(collider.gameObject);
        }

        private void TryApplyDamage(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                if (damagable.Team != _projectileData.Team)
                    damagable.ApplyDamage(_projectileData.Damage);
            }
        }

        private void DestroyProjectile() => Destroy(gameObject);

        #endregion
    }

    public class ProjectileData
    {
        public Team Team;
        public float BlastRadius;
        public float Damage;
        public float Speed;
    }
}