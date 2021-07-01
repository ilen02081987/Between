using UnityEngine;
using Between.StateMachine;

namespace Between.Enemies.Mavka
{
    public class IdleDetectionState : BaseState
    {
        private readonly Transform _owner;
        private readonly Transform _target;

        private float _detectionRadius => GameSettings.Instance.DetectionRadius;
        private bool _playerCloseEnough => Vector3.Distance(_owner.position, _target.position) <= _detectionRadius;

        public IdleDetectionState(FinitStateMachine stateMachine, Transform owner, Transform target)
            : base(stateMachine)
        {
            _owner = owner;
            _target = target;
        }

        public override void Update()
        {
            if (_playerCloseEnough)
                SwitchState(typeof(AttackState));
        }
    }
}