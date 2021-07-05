using UnityEngine;
using Between.StateMachine;

namespace Between.Enemies.Mavka
{
    public abstract class BaseIdleDetectionsState : BaseState
    {
        private readonly Transform _owner;
        private readonly Transform _target;

        private float _detectionRadius;
        private bool _targetCloseEnough => Vector3.Distance(_owner.position, _target.position) <= _detectionRadius;

        public BaseIdleDetectionsState(FinitStateMachine stateMachine, Transform owner, Transform target, float radius)
            : base(stateMachine)
        {
            _owner = owner;
            _target = target;
            _detectionRadius = radius;
        }

        public override void Update()
        {
            if (_target != null && _targetCloseEnough && _stateMachineEnabled)
                PerformOnCloseEnough();
        }

        protected abstract void PerformOnCloseEnough();
    }
}