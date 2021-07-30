using Between.Enemies.Mavka;
using Between.StateMachine;

namespace Between.Enemies.Bosses
{
    public class LichController : BossController
    {
        protected override FinitStateMachine InitMeleeStateMachine() => null;

        protected override FinitStateMachine InitRangeStateMachine()
        {
            var stateMachine = new FinitStateMachine();

            RangeIdleDetectionState idleState = new RangeIdleDetectionState
                (stateMachine, transform, player.transform, GameSettings.Instance.Lich.RangeDetectionRadius);

            SingleProjectileCaseState singleCastState
                = new SingleProjectileCaseState(stateMachine, animator, player.transform, _singleProjectileSpawnPoint, 
                GameSettings.Instance.Lich.SingleProjectileCastWeight, GameSettings.Instance.Lich.SingleProjectileCastTime);

            SeveralProjectilesCastState severalCastState
                = new SeveralProjectilesCastState(stateMachine, animator, player.transform,
                GameSettings.Instance.Lich.SeveralProjectilesCastWeight, GameSettings.Instance.Lich.SeveralProjectilesCastTime,
                GameSettings.Instance.Lich.SeveralProjectilesCastDelay, GameSettings.Instance.Lich.SeveralProjectilesSingleCast, _severalProjectileSpawnPoints);

            AttackState attackState = new AttackState(stateMachine, singleCastState, severalCastState);

            CooldownState cooldownState = new CooldownState(
                stateMachine, idleState, GameSettings.Instance.Lich.RangeCooldownBase,
                GameSettings.Instance.Lich.RangeCooldownShift);

            stateMachine.AddStates(idleState, singleCastState, severalCastState, attackState, cooldownState);
            return stateMachine;
        }
    }
}