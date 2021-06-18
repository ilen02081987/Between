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
        [SerializeField] private Team _team = Team.Player;
        [SerializeField] private float _damage = 6f;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _blastRadius = 2f;
        [SerializeField] private bool _friendlyFire = false;

        private Rigidbody _rigidbody;

        private Vector3 _direction;

        private bool _hasCollide = false;

        public Transform ImpactParticles;

        //projectile vfx
        private Transform _particleRotationChildObject;

        #region BEHAVIOUR

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _particleRotationChildObject = transform.GetChild(0);
        }

        public void Launch(Vector3 direction)
        {
            _direction = direction;
            //_particleRotationChildObject.rotation = Quaternion.LookRotation(direction);
            Vector3 newDirection = Vector3.RotateTowards(Vector3.forward, direction, 360, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        private void Update()
        {
            _rigidbody.velocity = _direction * _speed;
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
                if (damagable.Team != _team || _friendlyFire)
                {
                    ApplyDamage(damagable);
                    TakeImpactDamage();
                }
            }
        }

        private void ApplyDamage(IDamagable damagable)
        {
            if (damagable is Shield)
                (damagable as Shield).ApplyDamage(_damage, _blastRadius);
            else
                damagable.ApplyDamage(_damage);
        }

        private void TakeImpactDamage()
        {
            _health -= _impactDamage;

            if (_health <= 0f)
                DestroyProjectile();
        }

        private void DestroyProjectile()
        {
            Instantiate(ImpactParticles.GetChild(0), transform.position, Quaternion.identity);
            Instantiate(ImpactParticles.GetChild(1), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        #endregion
    }
}