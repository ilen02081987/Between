using System;
using UnityEngine;
using UnityEngine.AI;
using Between.Animations;
using Between.Damage;
using Between.SpellsEffects.ShieldSpell;
using Between.StateMachine;

namespace Between.Enemies.Skeletons
{
    public abstract class BaseSkeleton : BaseEnemy
    {
        [SerializeField] protected SkeletonData data;

        protected FinitStateMachine _stateMachine;

        protected bool _isTakeDamage => _stateMachine.CompareState(typeof(TakeDamageState));
        protected bool _isDestroingShield => _stateMachine.CompareState(typeof(DestroyShieldState));

        protected override void Start()
        {
            base.Start();

            data.Transform = transform;
            data.Player = player;
            data.Animator = animator;
            data.DamageItem = _damage;
            data.LocomotionController = new NpcLocomotionController(data.NavMeshAgent, data.Animator);

            InitStateMachine();
        }

        protected abstract void InitStateMachine();

        protected override void PerformOnDamage()
        {
            if (!_isTakeDamage)
                _stateMachine.SwitchState(typeof(TakeDamageState));
        }

        protected override void PerformOnDie()
        {
            _stateMachine.Disable();
            base.PerformOnDie();
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }

    [Serializable]
    public class SkeletonData
    {
        public NavMeshAgent NavMeshAgent;
        public float DetectionDistance;
        public float AttackDistance;
        public float CooldownTime;
        public NpcLocomotionController LocomotionController;
        public Transform[] WayPoints;
        public GameObject ArrowPrefab;
        public Transform ArrowSpawnPoint;
        
        [HideInInspector] public Shield Shield;
        [HideInInspector] public DamageItem DamageItem;
        [HideInInspector] public Transform Transform;
        [HideInInspector] public PlayerController Player;
        [HideInInspector] public NpcAnimator Animator;
    }
}