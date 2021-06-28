using System.Collections;
using UnityEngine;
using Between.Interfaces;
using Between.SpellsEffects.ShieldSpell;
using Between.Damage;

namespace Between.Enemies
{
    public class SkeletonMelee : BaseEnemy
    {
        private enum State
        {
            PatrolLeft = 0,
            PatrolRight,
            AggroLeft,
            AggroRight,
            Stay
        }

        [SerializeField] private float _damage;
        [SerializeField] private float _damageToShieldRadius;
        [SerializeField] private DamageType _damageType = DamageType.Sword;

        [SerializeField] private float patrolRange;
        [SerializeField] private float patrolSpeed;
        [SerializeField] private float aggroSpeed;
        [SerializeField] private float attackFrequency;
        [SerializeField] private bool isMovingRight;
        [SerializeField] private float checkRadius;
        [SerializeField] private float agroRange;

        [SerializeField] private PlayerController player;

        private bool _freeze = false;
        private float _speedMod = 0;
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (player == null || health <= 0)
                return;

            // можно ли выйти из кд атаки раньше если уничтожили все вокруг
            CheckCooldown();

            if (_speedMod <= 1)
                _speedMod += 1 * Time.deltaTime;

            switch (GetState(player.transform.position))
            {
                case State.PatrolLeft:
                    Translate(-patrolSpeed * _speedMod * Time.deltaTime);
                    break;
                case State.PatrolRight:
                    Translate(patrolSpeed * _speedMod * Time.deltaTime);
                    break;
                case State.AggroLeft:
                    Translate(-aggroSpeed * _speedMod * Time.deltaTime);
                    break;
                case State.AggroRight:
                    Translate(aggroSpeed * _speedMod * Time.deltaTime);
                    break;
                default:
                    break;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_freeze)
                TryApplyDamage(other.gameObject);
        }

        private void OnCollisionStay(Collision other)
        {
            if (!_freeze)
                TryApplyDamage(other.gameObject);
        }

        private State GetState(Vector3 playerPos)
        {
            // должно быть _freeze + проверка что рядом есть цель
            if (_freeze)
                return State.Stay;

            if (SeePlayer(playerPos))
            {
                if (PlayerToTheRight(playerPos))
                    return State.AggroRight;

                return State.AggroLeft;
            }

            if (isMovingRight)
            {
                if (transform.position.x <= (_startPosition.x + patrolRange))
                    return State.PatrolRight;

                _speedMod = 0;
                isMovingRight = false;
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

                return State.PatrolLeft;
            }


            if (!isMovingRight)
            {
                if (transform.position.x >= (_startPosition.x - patrolRange))
                    return State.PatrolLeft;

                _speedMod = 0;
                isMovingRight = true;

                return State.PatrolRight;
            }

            // в любой непонятной ситуации патрулируй
            return State.PatrolRight;
        }

        private IEnumerator AttackCooldown()
        {
            _freeze = true;
            yield return new WaitForSeconds(attackFrequency);
            _freeze = false;
        }

        // видит ли скелет игрока
        private bool SeePlayer(Vector3 playerPosition)
        {
            return Vector3.Distance(transform.position, playerPosition) < agroRange &&
                !Physics.Raycast(transform.position, (playerPosition - transform.position).normalized, Mathf.Infinity, 1 << 3);
        }

        // определяем в какую сторону к игроку двигаться
        private bool PlayerToTheRight(Vector3 playerPos)
        {
            return transform.position.x < playerPos.x;
        }

        private void TryApplyDamage(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                if (damagable.Team != Team)
                {
                    Attack(damagable);
                    StartCoroutine(AttackCooldown());
                    InvokeAttackEvent();
                }
            }
        }

        private void Attack(IDamagable damagable)
        {
            if (damagable is Shield)
            {
                (damagable as Shield).ApplyDamage(_damageType, _damage, _damageToShieldRadius);
                return;
            }

            damagable.ApplyDamage(_damageType, _damage);
            return;
        }

        // можно ли выйти из кд атаки раньше если уничтожили все вокруг
        private void CheckCooldown()
        {
            if (_freeze)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);
                bool isCollision = false;

                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.TryGetComponent<Shield>(out Shield s) || hitCollider.gameObject.TryGetComponent<Player>(out Player p))
                        isCollision = true;
                }

                if (!isCollision)
                    _freeze = false;
            }
        }

        private void Translate(float shift)
        {
            transform.Translate(shift, 0f, 0f);
        }
    }
}