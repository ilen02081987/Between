using Between.Animations;
using Between.StateMachine;

namespace Between.Enemies
{
    public class TakeDamageState : BaseState
    {
        private readonly NpcAnimator _animator;
        private readonly IState _next;

        public TakeDamageState(FinitStateMachine stateMachine, NpcAnimator animator, IState next) : base(stateMachine)
        {
            _animator = animator;
            _next = next;
        }

        public override void Enter()
        {
            _animator.TakeDamage(() =>
            {
                if (IsCurrentState && _stateMachineEnabled)
                    SwitchState(_next.GetType());
            });
        }
    }
}