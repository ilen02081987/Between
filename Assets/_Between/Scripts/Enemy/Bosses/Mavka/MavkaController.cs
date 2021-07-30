using UnityEngine;
using Between.StateMachine;
using Between.Enemies.Mavka;

namespace Between.Enemies.Bosses
{
    public class MavkaController : BossController
    {
        [Space]
        [SerializeField] private Transform _meleeCastPoint;
        [SerializeField] private Transform _meleeSpawnPoint;

        protected override void Start()
        {
            base.Start();

            InitRangeStateMachine();
            InitMeleeStateMachine();
        }

        protected override FinitStateMachine InitMeleeStateMachine()
        {
            var stateMachine = new FinitStateMachine();

            MeleeIdleDetectionState idleState = new MeleeIdleDetectionState
                (stateMachine, transform, player.transform, GameSettings.Instance.MeleeDetectionRadius);

            MeleeAttackState attackState
                = new MeleeAttackState(stateMachine, player.transform, _meleeCastPoint, _meleeSpawnPoint);

            CooldownState cooldownState = new CooldownState
                (stateMachine, idleState, GameSettings.Instance.MeleeCooldownBase,
                GameSettings.Instance.MeleeCooldownShift);

            stateMachine.AddStates(idleState, attackState, cooldownState);
            return stateMachine;
        }

        protected override FinitStateMachine InitRangeStateMachine()
        {
            var stateMachine = new FinitStateMachine();

            RangeIdleDetectionState idleState = new RangeIdleDetectionState
                (stateMachine, transform, player.transform, GameSettings.Instance.RangeDetectionRadius);

            SingleProjectileCaseState singleCastState
                = new SingleProjectileCaseState(stateMachine, animator, player.transform, _singleProjectileSpawnPoint,
                GameSettings.Instance.SingleProjectileCastWeight, GameSettings.Instance.MavkaSingleProjectileCastTime);

            SeveralProjectilesCastState severalCastState
                = new SeveralProjectilesCastState(stateMachine, animator, player.transform, 
                GameSettings.Instance.SeveralProjectilesCastWeight, GameSettings.Instance.SeveralProjectilesCastTime,
                GameSettings.Instance.SeveralProjectilesCastDelay, GameSettings.Instance.SeveralProjectilesSingleCast, _severalProjectileSpawnPoints);

            AttackState attackState = new AttackState(stateMachine, singleCastState, severalCastState);

            CooldownState cooldownState = new CooldownState(
                stateMachine, idleState, GameSettings.Instance.RangeCooldownBase,
                GameSettings.Instance.RangeCooldownShift);

            stateMachine.AddStates(idleState, singleCastState, severalCastState, attackState, cooldownState);
            return stateMachine;
        }
    }
}