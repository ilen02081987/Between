using Between.Enemies.Mavka;
using Between.StateMachine;
using UnityEngine;

namespace Between.Enemies.Skeletons
{
    public class IdleDetectionState : BaseIdleDetectionsState
    {
        public IdleDetectionState(FinitStateMachine stateMachine, Transform owner, Transform target, float radius)
            : base(stateMachine, owner, target, radius) { }

        protected override void PerformOnCloseEnough() => SwitchState(typeof(ChasingState));
    }
}