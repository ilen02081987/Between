using System.Collections;
using UnityEngine;
using Between.Teams;
using Between.Interfaces;
using Between.SpellsEffects.ShieldSpell;
using System;

public class SkeletonMelee: BaseEnemy
{
    //const int statePatrolLeft = 0;
    //const int statePatrolRight = 1;
    //const int stateAggroLeft = 2;
    //const int stateAggroRight = 3;
    //const int stateAttack = 4;
    //const int stateCoolddown = 5;

    const int actionPatrolLeft = 0;
    const int actionPatrolRight = 1;
    const int actionAggroLeft = 2;
    const int actionAggroRight = 3;
    const int actionStay = 6;
    // состояние скелета 
    // 0 - патрулируем влево
    // 1 - патрулируем вправо
    // 2 - идем к игроку влево
    // 3 - идем к игроку вправо
    // 4 - атака по игроку
    // 5 - атака по щиту
    // 6 - кд удара, стоим на месте

    private bool _freeze = false;
    private float _speedMod = 0;
    private Vector3 _startPosition;

    public float _health;
    public float damageToPlayer;
    public float damageToShield;
    public float damageToShieldRadius;
    public float patrolRange;
    public float patrolSpeed;
    public float aggroSpeed;
    public float attackFrequency;
    public bool isMovingRight;
    public float checkRadius;
    public float agroRange;

    public Player player;

    public override Team Team { get; set; } = Team.Enemies;

    void Start()
    {
        _startPosition = transform.position;
    }

    // в зависимости от состояния возвращаем действие моба
    // 0 - патрулируем влево
    // 1 - патрулируем вправо
    // 2 - идем к игроку влево
    // 3 - идем к игроку вправо

    int getAction(Vector3 playerPos)
    {
        // должно быть _freeze + проверка что рядом есть цель
        if (_freeze)
        {
            return actionStay;
        }

        if (seePlayer(playerPos))
        {

            if (playerToTheRight(playerPos))
            {
                return actionAggroRight;
            }

            return actionAggroLeft;
        }

        if (isMovingRight)
        {
            if (transform.position.x <= (_startPosition.x + patrolRange))
            {
                return actionPatrolRight;
            }

            _speedMod = 0;
            isMovingRight = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            return actionPatrolLeft;

        }


        if (!isMovingRight)
        {
            if (transform.position.x >= (_startPosition.x - patrolRange))
            {
                return actionPatrolLeft;
            }

            _speedMod = 0;
            isMovingRight = true;
            return actionPatrolRight;

        }

        // в любой непонятной ситуации патрулируй
        return actionPatrolRight;

    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackFrequency);
        _freeze = false;
    }


    // видит ли скелет игрока
    bool seePlayer(Vector3 playerPos)
    {
        if (Vector3.Distance(transform.position, playerPos) < agroRange && !Physics.Raycast(transform.position, (playerPos - transform.position).normalized, Mathf.Infinity, 1 << 3))
        {
            return true;
        }

        return false;

    }

    // определяем в какую сторону к игроку двигаться
    bool playerToTheRight(Vector3 playerPos)
    {
        if (transform.position.x < playerPos.x) { 
            return true;
        }

        return false;

    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_freeze)
        {
            TryApplyDamage(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!_freeze)
        {
            TryApplyDamage(other.gameObject);
        }
    }

    private void TryApplyDamage(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            if (damagable.Team != Team) { 
                Attack(damagable);
                _freeze = true;
                StartCoroutine(attackCooldown());

                InvokeAttackEvent();
            }
        }
    }

    private void Attack(IDamagable damagable)
    {
        if (damagable is Shield) { 
            (damagable as Shield).ApplyDamage(damageToShield, damageToShieldRadius);
            return;
        }
        damagable.ApplyDamage(damageToPlayer);
        return;
    }

    // можно ли выйти из кд атаки раньше если уничтожили все вокруг
    private void checkCooldown()
    {
        if (_freeze)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);
            bool isCollision = false;
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.TryGetComponent<Shield>(out Shield s) || hitCollider.gameObject.TryGetComponent<Player>(out Player p))
                {
                    isCollision = true;
                }
            }

            if (!isCollision)
            {
                _freeze = false;
            }

        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // можно ли выйти из кд атаки раньше если уничтожили все вокруг
        checkCooldown();

        if (_speedMod <= 1)
            _speedMod += 1 * Time.deltaTime;

        int state = getAction(player.transform.position);

        // 0 - патрулируем влево
        // 1 - патрулируем вправо
        // 2 - идем к игроку влево
        // 3 - идем к игроку вправо

        switch (state)
        {
            case actionPatrolLeft:
                transform.position = new Vector3(transform.position.x - (patrolSpeed * _speedMod * Time.deltaTime), transform.position.y, transform.position.z);
                break;
            case actionPatrolRight:
                transform.position = new Vector3(transform.position.x + (patrolSpeed * _speedMod * Time.deltaTime), transform.position.y, transform.position.z);
                break;
            case actionAggroLeft:
                transform.position = new Vector3(transform.position.x - (aggroSpeed * _speedMod * Time.deltaTime), transform.position.y, transform.position.z);
                break;
            case actionAggroRight:
                transform.position = new Vector3(transform.position.x + (aggroSpeed * _speedMod * Time.deltaTime), transform.position.y, transform.position.z);
                break;
            default:
                break;
        }
    }
}
