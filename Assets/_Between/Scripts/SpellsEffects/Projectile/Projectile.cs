using System;
using System.Collections;
using UnityEngine;
using Between.Damage;
using Between.Interfaces;
using Between.SpellsEffects.ShieldSpell;
using Between.Teams;

namespace Between.SpellsEffects.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public float SizeZ => transform.localScale.z;
        public float SizeX => transform.localScale.x;
        public Team Team => _team;

        [SerializeField] private float _health = 10f;
        [SerializeField] private float _impactDamage = 5f;
        [SerializeField] private Team _team = Team.Player;
        [SerializeField] private DamageItem _damage;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _blastRadius = 2f;
        [SerializeField] private bool _friendlyFire = false;
        [SerializeField] private float _lifeTime = 15f;

        private Rigidbody _rigidbody;

        private Vector3 _direction;

        private bool _hasCollide = false;

        public event Action<Vector3> OnLaunch;
        public event Action OnDestroyed;

        public void Launch(Vector3 direction)
        {
            _direction = direction;
            OnLaunch?.Invoke(_direction);
        }

        public void ChangeDamageValue(float to) => _damage.Value = to;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            StartCoroutine(WaitToDestroy());
        }

        private void Update()
        {
            _rigidbody.velocity = _direction * _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_hasCollide)
                TryApplyDamage(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            _hasCollide = false;
        }

        private void TryApplyDamage(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                if (damagable.Team != _team || _friendlyFire)
                {
                    _hasCollide = true;

                    ApplyDamage(damagable);
                    TakeImpactDamage();
                    ApplySpecialEffects(gameObject);
                }
            }
            else if (gameObject.CompareTag("Ground"))
            {
                DestroyProjectile();
            }
        }

        protected virtual void ApplySpecialEffects(GameObject gameObject) { }

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
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);

            if (this != null && gameObject != null)
                DestroyProjectile();
        }
    }
}