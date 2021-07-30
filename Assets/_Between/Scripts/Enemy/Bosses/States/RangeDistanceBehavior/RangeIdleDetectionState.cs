using UnityEngine;
using Between.StateMachine;

namespace Between.Enemies.Mavka
{
    public class RangeIdleDetectionState : BaseIdleDetectionsState
    {
        public RangeIdleDetectionState(FinitStateMachine stateMachine, Transform owner, Transform target, float radius)
            : base(stateMachine, owner, target, radius) { }

        protected override void PerformOnCloseEnough() => SwitchState(typeof(AttackState));
    }
}