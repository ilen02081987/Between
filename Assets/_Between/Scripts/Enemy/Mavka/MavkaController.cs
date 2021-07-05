using Between.StateMachine;
using UnityEngine;

namespace Between.Enemies.Mavka
{
    public class MavkaController : BaseEnemy
    {
        [SerializeField] private Transform[] _severalProjectileSpawnPoints;
        [SerializeField] private Transform _singleProjectileSpawnPoint;

        private FinitStateMachine _stateMachine;
        private Transform _player;

        protected override void Start()
        {
            _player = App.Instance.PlayerController.transform;
            _stateMachine = new FinitStateMachine();

            IdleDetectionState idleState = new IdleDetectionState(_stateMachine, transform, _player);
            SingleProjectileCaseState singleCastState = new SingleProjectileCaseState(_stateMachine, _player, _singleProjectileSpawnPoint);
            SeveralProjectilesCastState severalCastState = new SeveralProjectilesCastState(_stateMachine, _player, _severalProjectileSpawnPoints);
            AttackState attackState = new AttackState(_stateMachine, transform, _player, singleCastState, severalCastState);
            CooldownState cooldownState = new CooldownState(_stateMachine);

            _stateMachine.AddStates(idleState, singleCastState, severalCastState, attackState, cooldownState);

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
            base.PerformOnDie();
        }

        private void TryRotate()
        {
            if (_player == null)
                return;

            Quaternion rotation = transform.rotation;
            int direction = transform.position.x >= _player.position.x ? -1 : 1;
            var newRotation = Quaternion.Euler(0f, direction * 90f, 0f);

            if (newRotation != rotation)
                transform.rotation = newRotation;
        }
    }
}