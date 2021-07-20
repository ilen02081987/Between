using System;
using UnityEngine;
using UnityEngine.AI;
using Between.Animations;
using Between.Damage;
using Between.SpellsEffects.ShieldSpell;

namespace Between.Enemies.Skeletons
{
    public class BaseSkeleton : BaseEnemy
    {
        [SerializeField] protected SkeletonData data;

        protected override void Start()
        {
            base.Start();

            data.Transform = transform;
            data.Player = player;
            data.Animator = animator;
            data.DamageItem = _damage;
            data.LocomotionController = new NpcLocomotionController(data.NavMeshAgent, data.Animator);
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
        
        [HideInInspector] public Shield Shield;
        [HideInInspector] public DamageItem DamageItem;
        [HideInInspector] public Transform Transform;
        [HideInInspector] public PlayerController Player;
        [HideInInspector] public NpcAnimator Animator;
    }
}