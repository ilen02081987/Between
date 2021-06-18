using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.Teams;

public class RangedEnemy : BaseEnemy
{
    public override Team Team { get; set; } = Team.Enemies;

    public float agroRange;
    public Transform target;
    public Transform spawnPoint;

    public float attackDelay;
    public GameObject arrow;

    private bool isAgred;
    private float attackCD;
    private ProjectileSpawner _spawner;

    public int startDelay = 0;
    public int delayCount = 0;



    void Start()
    {
        _spawner = new ProjectileSpawner("Arrow", 0);
    }

    void Update()
    {
        // никогда так не делайте
        if (delayCount < startDelay)
        {
            delayCount++;
            return;
        }

        if (target == null || health <= 0)
        {
            return;
        }

        TryRotate();

        if (Vector3.Distance(transform.position, target.transform.position) < agroRange)
        {
            if (!Physics.Raycast(transform.position, (target.position - transform.position).normalized, Mathf.Infinity, 1 << 3))
                isAgred = true;
        }
        else
        {
            isAgred = false;
        }

        if (isAgred)
            Shoot();

        if (attackCD > 0)
            attackCD -= 1*Time.deltaTime ;
    }

    private void Shoot()
    {
        if (attackCD <= 0)
        {
            _spawner.Spawn(spawnPoint.position, (target.position - spawnPoint.position).normalized);
            attackCD = attackDelay;

            InvokeAttackEvent();
        }
    }

    private void TryRotate()
    {
        int direction = transform.position.x >= target.position.x ? -1 : 1;
        transform.rotation = Quaternion.Euler(0f, direction * 90f, 0f);
    }
}
