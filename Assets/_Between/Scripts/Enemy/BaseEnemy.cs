using Between.Animations;
using Between.Damage;
using Between.Teams;
using System;
using UnityEngine;
using UnityEngine.AI;

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

        protected virtual void Start()
        {
            player = Player.Instance.Controller;
            animator.AttachTo(this);

            InitDamagableObject();
        }
        
        protected override void PerformOnDie()
        {
            Destroy(gameObject, _destroyTime);
        }
    }
}