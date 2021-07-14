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
        public event Action<Action> OnAttack;
        public event Action OnMove;
        public override Team Team => Team.Enemies;

        protected Transform _player { get; private set; }

        [SerializeField] protected NpcAnimator _animator;
        [SerializeField] protected DamageItem _damage;
        [SerializeField] protected NavMeshAgent _navMeshAgent;

        [SerializeField] protected float _detectionDistance;
        [SerializeField] protected float _attackDistance;
        [SerializeField] protected float _cooldownTime;

        [SerializeField] private float _destroyTime = 2;

        protected override void Start()
        {
            _player = Player.Instance.Controller.transform;
            _animator.AttachTo(this);
            base.Start();
        }

        protected void InvokeAttackEvent(Action action) => OnAttack?.Invoke(action);
        protected void InvokeMoveEvent() => OnMove?.Invoke();

        protected override void PerformOnDamage()
        {
            InvokeDamageEvent();
        }

        protected override void PerformOnDie()
        {
            Destroy(gameObject, _destroyTime);
            InvokeDieEvent();
        }
    }
}