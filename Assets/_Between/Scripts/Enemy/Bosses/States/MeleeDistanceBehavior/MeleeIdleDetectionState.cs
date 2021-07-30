using UnityEngine;
using Between.StateMachine;
using Between.Enemies.Mavka;

namespace Between.Enemies.Mavka
{
    public class MeleeIdleDetectionState : BaseIdleDetectionsState
    {
        public MeleeIdleDetectionState(FinitStateMachine stateMachine, Transform owner, Transform target, float radius)
            : base(stateMachine, owner, target, radius) { }

        protected override void PerformOnCloseEnough() => SwitchState(typeof(MeleeAttackState));
    }
}