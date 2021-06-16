using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Between.Teams;
using Between.Interfaces;
using Between.SpellsEffects.ShieldSpell;

public class SkeletonMelee: MonoBehaviour, IDamagable
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
    const int actionAttackPlayer = 4;
    const int actionAttackShield = 5;
    const int actionStay = 6;
    // состояние скелета 
    // 0 - патрулируем влево
    // 1 - патрулируем вправо
    // 2 - идем к игроку влево
    // 3 - идем к игроку вправо
    // 4 - атака по игроку
    // 5 - атака по щиту
    // 6 - кд удара, стоим на месте

    private int _state;
    private bool _freeze = false;
    private bool _hasCollide = false;
    private float _speedMod = 0;
    private int _action;
    private Vector3 _startPosition;
    private Player _playerScript;

    public float _health;
    public int damageToPlayer;
    public int damageToShield;
    public int damageToShieldRadius;
    public float patrolRange;
    public float patrolSpeed;
    public float aggroSpeed;
    public float attackFrequency;
    public bool isMovingRight;
    public float agroRange;

    public GameObject player;

    public Team Team { get; set; } = Team.Enemies;

    public void ApplyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerScript = player.GetComponent<Player>();
        _startPosition = transform.position;
    }

    // в зависимости от состояния возвращаем действие моба
    // 0 - патрулируем влево
    // 1 - патрулируем вправо
    // 2 - идем к игроку влево
    // 3 - идем к игроку вправо
    // 4 - атака по игроку
    // 5 - атака по щиту
    // 6 - кд удара, стоим на месте
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

    private void OnCollisionExit(Collision other)
    {
        _freeze = false;
    }

    private void TryApplyDamage(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            Attack(damagable);
            _freeze = true;
            StartCoroutine(attackCooldown());
        }
        
    }

    private void Attack(IDamagable damagable)
    {
        if (damagable is Shield) { 
            (damagable as Shield).ApplyDamage(damageToShield, damageToShieldRadius);
            Debug.Log("Attack Shield");
            return;
        }
        Debug.Log("Attack Player");
        damagable.ApplyDamage(damageToPlayer);
        return;
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            return;
        }

        if (_health == 0)
        {
            Destroy(gameObject);
        }

        
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
