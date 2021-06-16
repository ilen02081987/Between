using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Between.Teams;
using Between.Interfaces;

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
    // ��������� ������� 
    // 0 - ����������� �����
    // 1 - ����������� ������
    // 2 - ���� � ������ �����
    // 3 - ���� � ������ ������
    // 4 - ����� �� ������
    // 5 - ����� �� ����
    // 6 - �� �����, ����� �� �����

    private int _state;
    private bool _freeze = false;
    private int _action;
    private Vector3 _startPosition;

    public float _health;
    public int damageToPlayer;
    public int damageToShield;
    public float patrolRange;
    public float patrolSpeed;
    public float aggroSpeed;
    public float attackFrequency;
    public bool isMovingRight;

    private float _speedMod = 0;

    public float agroRange;
    public float minAttackRangeToPlayer; // ���������� �� ������ ��� ������� ������� ��� ����� ������� � ���� ����
    public float minAttackRangeToShield; // ���������� �� ���� ��� ������� ������� ��� ����� ������� � ���� ����

    public GameObject player;
    private Player playerScript;

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
        playerScript = player.GetComponent<Player>();
        _startPosition = transform.position;
    }

    // � ����������� �� ��������� ���������� �������� ����
    // 0 - ����������� �����
    // 1 - ����������� ������
    // 2 - ���� � ������ �����
    // 3 - ���� � ������ ������
    // 4 - ����� �� ������
    // 5 - ����� �� ����
    // 6 - �� �����, ����� �� �����
    int getState(Vector3 playerPos)
    {
        // ������ ���� _freeze + �������� ��� ����� ���� ����
        if (_freeze)
        {
            return actionStay;
        }

        if (closeToPlayer(playerPos))
        {
            return actionAttackPlayer;
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
            else
            {
                _speedMod = 0;
                isMovingRight = false;
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                return actionPatrolLeft;

            }

        }


        if (!isMovingRight)
        {
            if (transform.position.x >= (_startPosition.x - patrolRange))
            {
                return actionPatrolLeft;
            }
            else
            {
                _speedMod = 0;
                isMovingRight = true;
                return actionPatrolRight;
            }

        }
        return actionAggroRight;

    }

    void attackPlayer(Player playerScript)
    {
        playerScript.ApplyDamage(damageToPlayer);
        _freeze = true;
        StartCoroutine(attackCooldown());
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackFrequency);
        _freeze = false;
    }


    // ����� �� ������ ������
    bool seePlayer(Vector3 playerPos)
    {
        if (Vector3.Distance(transform.position, playerPos) < agroRange && !Physics.Raycast(transform.position, (playerPos - transform.position).normalized, Mathf.Infinity, 1 << 3))
        {
            return true;
        }

        return false;

    }

    // ���������� � ����� ������� � ������ ���������
    bool playerToTheRight(Vector3 playerPos)
    {
        if (transform.position.x < playerPos.x) { 
            return true;
        }

        return false;

    }

    // ����� �� ��� ������ ����� ���������
    bool closeToPlayer(Vector3 playerPos)
    {
        if (Vector3.Distance(transform.position, playerPos) > minAttackRangeToPlayer)
        {
            return false;
        }

        return true;

    }

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

        int state = getState(player.transform.position);
        // 0 - ����������� �����
        // 1 - ����������� ������
        // 2 - ���� � ������ �����
        // 3 - ���� � ������ ������
        // 4 - ����
        // 5 - �� �����, ����� �� �����

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
            case actionAttackPlayer:
                attackPlayer(playerScript);
                break;
        }
    }
}
