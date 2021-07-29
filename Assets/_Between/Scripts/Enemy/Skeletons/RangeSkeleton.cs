using Between.StateMachine;

namespace Between.Enemies.Skeletons
{
    public class RangeSkeleton : BaseSkeleton
    {
        protected override void InitStateMachine()
        {
            stateMachine = new FinitStateMachine();

            BaseState idleState = null;

            if (data.WayPoints != null && data.WayPoints.Length > 1)
                idleState = new PatrolState(stateMachine, data);
            else
                idleState = new IdleDetectionState(stateMachine, data);

            var attackState = new RangeAttackState(stateMachine, data);
            var chasingState = new ChasingState(stateMachine, data, attackState);
            var cooldownState = new ChasingCooldownState(stateMachine, data, attackState);
            var takeDamageState = new TakeDamageState(stateMachine, data, chasingState);

            stateMachine.AddStates(idleState, chasingState, attackState, cooldownState, takeDamageState);
        }
    }
}