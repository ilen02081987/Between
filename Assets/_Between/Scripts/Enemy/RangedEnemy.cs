using UnityEngine;
using Between.SpellsEffects.Projectile;

namespace Between.Enemies
{
    public class RangedEnemy : BaseEnemy
    {
        [SerializeField] private float agroRange;
        [SerializeField] private Transform target;
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private float attackDelay;
        [SerializeField] private GameObject arrow;

        [SerializeField] private int fireDelay;

        private bool isAgred;
        private float attackCD;
        private ProjectileSpawner _spawner;

        protected override void Start()
        {
            _spawner = new ProjectileSpawner(arrow.gameObject.name, 0);

            base.Start();
        }

        void Update()
        {
            if (target == null || Health <= 0)
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
                attackCD -= 1 * Time.deltaTime;
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
}