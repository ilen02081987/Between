using UnityEngine;
using Between.StateMachine;

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

        protected override void Start()
        {
            base.Start();

            InitRangeStateMachine();
            InitMeleeStateMachine();
        }

        private void InitRangeStateMachine()
        {
            _rangeStateMachine = new FinitStateMachine();

            RangeIdleDetectionState idleState = new RangeIdleDetectionState
                (_rangeStateMachine, transform, player.transform, GameSettings.Instance.RangeDetectionRadius);
            
            SingleProjectileCaseState singleCastState 
                = new SingleProjectileCaseState(_rangeStateMachine, player.transform, _singleProjectileSpawnPoint);
            
            SeveralProjectilesCastState severalCastState 
                = new SeveralProjectilesCastState(_rangeStateMachine, player.transform, _severalProjectileSpawnPoints);
            
            AttackState attackState = new AttackState(_rangeStateMachine, singleCastState, severalCastState);
            
            CooldownState cooldownState = new CooldownState(
                _rangeStateMachine, idleState, GameSettings.Instance.RangeCooldownBase, 
                GameSettings.Instance.RangeCooldownShift);

            _rangeStateMachine.AddStates(idleState, singleCastState, severalCastState, attackState, cooldownState);
        }

        private void InitMeleeStateMachine()
        {
            _meleeStateMachine = new FinitStateMachine();

            MeleeIdleDetectionState idleState = new MeleeIdleDetectionState
                (_meleeStateMachine, transform, player.transform, GameSettings.Instance.MeleeDetectionRadius);

            MeleeAttackState attackState 
                = new MeleeAttackState(_meleeStateMachine, player.transform, _meleeCastPoint, _meleeSpawnPoint);

            CooldownState cooldownState = new CooldownState
                (_meleeStateMachine, idleState, GameSettings.Instance.MeleeCooldownBase, 
                GameSettings.Instance.MeleeCooldownShift);

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
            if (player == null)
                return;

            Quaternion rotation = transform.rotation;
            int direction = transform.position.x >= player.Position.x ? -1 : 1;
            var newRotation = Quaternion.Euler(0f, direction * 90f, 0f);

            if (newRotation != rotation)
                transform.rotation = newRotation;
        }
    }
}