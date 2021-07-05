using Between.StateMachine;
using UnityEngine;

namespace Between.Enemies.Mavka
{
    public class MavkaController : BaseEnemy
    {
        [SerializeField] private Transform[] _severalProjectileSpawnPoints;
        [SerializeField] private Transform _singleProjectileSpawnPoint;
        [Space]
        [SerializeField] private Transform _meleeCastPoint;
        [SerializeField] private Transform _meleeSpawnPoint;

        private FinitStateMachine _rangeStateMachine;
        private FinitStateMachine _meleeStateMachine;
        private Transform _player;

        protected override void Start()
        {
            _player = App.Instance.PlayerController.transform;

            InitRangeStateMachine();
            InitMeleeStateMachine();

            base.Start();
        }

        private void InitRangeStateMachine()
        {
            _rangeStateMachine = new FinitStateMachine();

            RangeIdleDetectionState idleState = new RangeIdleDetectionState
                (_rangeStateMachine, transform, _player, GameSettings.Instance.RangeDetectionRadius);
            
            SingleProjectileCaseState singleCastState 
                = new SingleProjectileCaseState(_rangeStateMachine, _player, _singleProjectileSpawnPoint);
            
            SeveralProjectilesCastState severalCastState 
                = new SeveralProjectilesCastState(_rangeStateMachine, _player, _severalProjectileSpawnPoints);
            
            AttackState attackState = new AttackState(_rangeStateMachine, singleCastState, severalCastState);
            
            CooldownState cooldownState = new CooldownState(
                _rangeStateMachine, GameSettings.Instance.RangeCooldownBase, 
                GameSettings.Instance.RangeCooldownShift, idleState);

            _rangeStateMachine.AddStates(idleState, singleCastState, severalCastState, attackState, cooldownState);
        }

        private void InitMeleeStateMachine()
        {
            _meleeStateMachine = new FinitStateMachine();

            MeleeIdleDetectionState idleState = new MeleeIdleDetectionState
                (_meleeStateMachine, transform, _player, GameSettings.Instance.MeleeDetectionRadius);

            MeleeAttackState attackState 
                = new MeleeAttackState(_meleeStateMachine, _player, _meleeCastPoint, _meleeSpawnPoint);

            CooldownState cooldownState = new CooldownState
                (_meleeStateMachine, GameSettings.Instance.MeleeCooldownBase, 
                GameSettings.Instance.MeleeCooldownShift, idleState);

            _meleeStateMachine.AddStates(idleState, attackState, cooldownState);
        }

        private void Update()
        {
            _rangeStateMachine.Update();
            _meleeStateMachine.Update();

            TryRotate();
        }

        protected override void PerformOnDie()
        {
            _rangeStateMachine.Disable();
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