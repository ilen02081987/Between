using Between.Animations;
using Between.Damage;
using Between.SpellsEffects.ShieldSpell;
using System;
using UnityEngine;
using UnityEngine.AI;

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
        }
    }

    [Serializable]
    public class SkeletonData
    {
        public NavMeshAgent NavMeshAgent;
        public float DetectionDistance;
        public float AttackDistance;
        public float CooldownTime;

        [HideInInspector] public Shield Shield;
        [HideInInspector] public DamageItem DamageItem;
        [HideInInspector] public Transform Transform;
        [HideInInspector] public PlayerController Player;
        [HideInInspector] public NpcAnimator Animator;
    }
}