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

        protected FinitStateMachine stateMachine;

        protected bool _isTakeDamage => stateMachine.CompareState(typeof(TakeDamageState));

        protected override void Start()
        {
            base.Start();

            data.Transform = transform;
            data.Player = player;
            data.Animator = animator;
            data.DamageItem = _damage;
            data.LocomotionController = new NpcLocomotionController(data.NavMeshAgent, data.Animator);

            InitNpc();
            InitStateMachine();
        }

        protected abstract void InitStateMachine();
        protected virtual void InitNpc() { }

        protected override void PerformOnDamage()
        {
            if (!_isTakeDamage)
                stateMachine.SwitchState(typeof(TakeDamageState));
        }

        protected override void PerformOnDie()
        {
            stateMachine.Disable();
            base.PerformOnDie();
        }

        protected override void PerformOnPlayerDie()
        {
            stateMachine.Disable();
        }

        private void Update()
        {
            stateMachine.Update();
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
        public AudioClip AttackSound;
        public AudioClip HitSound;

        [HideInInspector] public Shield Shield;
        [HideInInspector] public DamageItem DamageItem;
        [HideInInspector] public Transform Transform;
        [HideInInspector] public PlayerController Player;
        [HideInInspector] public NpcAnimator Animator;
    }
}