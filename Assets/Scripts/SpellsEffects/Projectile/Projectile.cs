using Between.Interfaces;
using Between.SpellsEffects.ShieldSpell;
using Between.Teams;
using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _health = 10f;
        [SerializeField] private float _impactDamage = 5f;

        [SerializeField] private bool _friendlyFire = false;

        private Rigidbody _rigidbody;

        private ProjectileData _projectileData;
        private Vector3 direction;

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

        private void OnTriggerEnter(Collider other)
        {
            if (!_hasCollide)
            {
                _hasCollide = true;
                TryApplyDamage(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _hasCollide = false;
        }

        #endregion

        #region PRIVATE METHODS

        private void TryApplyDamage(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                if (damagable.Team != _projectileData.Team || _friendlyFire)
                {
                    ApplyDamage(damagable);
                    TakeImpactDamage();
                }
            }
        }

        private void ApplyDamage(IDamagable damagable)
        {
            if (damagable is Shield)
                (damagable as Shield).ApplyDamage(_projectileData.Damage, _projectileData.BlastRadius);
            else
                damagable.ApplyDamage(_projectileData.Damage);
        }

        private void TakeImpactDamage()
        {
            _health -= _impactDamage;

            if (_health <= 0f)
                DestroyProjectile();
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