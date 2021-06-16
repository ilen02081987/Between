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

    public float attackDelay;
    private float attackCD;
    public GameObject arrow;


    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    void Shooting()
    {
        if (attackCD <= 0)
        {
            new ProjectileSpawner("Projectile");
            attackCD = attackDelay;
        }
    }
}
