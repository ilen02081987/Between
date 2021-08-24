using Between.Enemies.Mavka;
using Between.StateMachine;
using System;
using UnityEngine;

namespace Between.Enemies.Bosses
{
    public class LichController : BossController
    {
        [SerializeField] private BaseEnemy _castedSkeleton;
        [SerializeField] private Transform[] _skeletonsCastPoints;

        protected override FinitStateMachine InitMeleeStateMachine() => null;

        protected override FinitStateMachine InitRangeStateMachine()
        {
            var lichSettings = GameSettings.Instance.Lich;
            var stateMachine = new FinitStateMachine();

            RangeIdleDetectionState idleState = new RangeIdleDetectionState
                (stateMachine, transform, player.transform, lichSettings.RangeDetectionRadius);

            SingleProjectileCaseState singleCastState
                = new SingleProjectileCaseState(stateMachine, animator, player.transform, _singleProjectileSpawnPoint,
                lichSettings.SingleProjectileCastWeight, lichSettings.SingleProjectileCastTime);

            SeveralProjectilesCastState severalCastState
                = new SeveralProjectilesCastState(stateMachine, animator, player.transform,
                lichSettings.SeveralProjectilesCastWeight, lichSettings.SeveralProjectilesCastTime,
                lichSettings.SeveralProjectilesCastDelay, lichSettings.SeveralProjectilesSingleCast, _severalProjectileSpawnPoints);

            SkeletonsCallCastState skeletonsCallCastState
                = new SkeletonsCallCastState(stateMachine, animator, lichSettings.SkeletonsCallCastWeight,
                lichSettings.SkeletonsCastDelay, lichSettings.SkeletonsCastTime, lichSettings.SkeletonsSingleCast,
                _castedSkeleton, _skeletonsCastPoints);

            AttackState attackState = new AttackState(stateMachine, singleCastState, severalCastState, skeletonsCallCastState);

            CooldownState cooldownState = new CooldownState(
                stateMachine, idleState, lichSettings.RangeCooldownBase,
                lichSettings.RangeCooldownShift);

            stateMachine.AddStates(idleState, singleCastState, severalCastState, skeletonsCallCastState, attackState, cooldownState);
            return stateMachine;
        }
    }
}