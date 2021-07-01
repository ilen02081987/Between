using Between.StateMachine;
using UnityEngine;

namespace Between.Enemies.Mavka
{
    public class MavkaController : BaseEnemy
    {
        [SerializeField] private Transform _singleProjectileSpawnPoint;

        private FinitStateMachine _stateMachine;
        private Transform _player;

        protected override void Start()
        {
            _player = App.Instance.PlayerController.transform;
            _stateMachine = new FinitStateMachine();

            IdleDetectionState idleState = new IdleDetectionState(_stateMachine, transform, _player);
            TestCastState testCastState = new TestCastState(_stateMachine, _singleProjectileSpawnPoint, _player);
            AttackState attackState = new AttackState(_stateMachine, transform, _player, testCastState);
            CooldownState cooldownState = new CooldownState(_stateMachine);

            _stateMachine.AddStates(idleState, testCastState, attackState, cooldownState);

            base.Start();
        }

        private void Update()
        {
            _stateMachine.Update();
            TryRotate();
        }

        protected override void PerformOnDie()
        {
            _stateMachine.Disable();
        }

        private void TryRotate()
        {
            Quaternion rotation = transform.rotation;
            int direction = transform.position.x >= _player.position.x ? -1 : 1;
            var newRotation = Quaternion.Euler(0f, direction * 90f, 0f);

            if (newRotation != rotation)
                transform.rotation = newRotation;
        }
    }
}