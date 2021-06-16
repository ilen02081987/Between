using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.Teams;

public class RangedEnemyTest : MonoBehaviour
{
    private bool isAgred;
    public float agroRange;
    public Transform target;
    public Transform spawnPoint;

    public float attackDelay;
    private float attackCD;
    public GameObject arrow;

    public float hp;

    ProjectileSpawner _spawner;


    // Start is called before the first frame update
    void Start()
    {
        _spawner = new ProjectileSpawner(arrow.gameObject.name);
    }

    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < agroRange)
        {
            if (!Physics.Raycast(transform.position, (target.position - transform.position).normalized, Mathf.Infinity, 1 << 3))
            {
                isAgred = true;
            }
        }
        else
            isAgred = false;

        if (isAgred)
        {
            Shooting();
        }

        if (attackCD > 0)
        {
            attackCD -= 1*Time.deltaTime ;
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        }    
    }

    void Shooting()
    {
        if (attackCD <= 0)
        {
            _spawner.Spawn(spawnPoint.position, (target.position - transform.position).normalized);
            attackCD = attackDelay;
        }
    }
}
