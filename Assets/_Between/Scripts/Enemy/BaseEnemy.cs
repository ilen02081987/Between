using UnityEngine;
using UnityEngine.AI;
using Between.Animations;
using Between.Damage;
using Between.Teams;

namespace Between.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BaseEnemy : BaseDamagableObject
    {
        public override Team Team => Team.Enemies;

        protected PlayerController player { get; private set; }

        [SerializeField] protected NpcAnimator animator;
        [SerializeField] protected DamageItem _damage;

        [SerializeField] private float _destroyTime = 2;

        private Collider _collider;

        protected virtual void Start()
        {
            _collider = GetComponent<Collider>();
            player = Player.Instance.Controller;
            animator.AttachTo(this);

            InitDamagableObject();
        }
        
        protected override void PerformOnDie()
        {
            _collider.isTrigger = true;
            Destroy(gameObject, _destroyTime);
        }
    }
}