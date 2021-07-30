using Between.Enemies.Mavka;
using Between.StateMachine;
using UnityEngine;

namespace Between.Enemies.Skeletons
{
    public class IdleDetectionState : BaseIdleDetectionsState
    {
        public IdleDetectionState(FinitStateMachine stateMachine, SkeletonData data)
            : base(stateMachine, data.Transform, data.Player.transform, data.DetectionDistance) { }

        protected override void PerformOnCloseEnough() => SwitchState(typeof(ChasingState));
    }
}