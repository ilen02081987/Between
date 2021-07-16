using Between.Animations;
using Between.Enemies.Skeletons;
using Between.StateMachine;

namespace Between.Enemies
{
    public class TakeDamageState : BaseState
    {
        private readonly NpcAnimator _animator;
        private readonly IState _next;

        public TakeDamageState(FinitStateMachine stateMachine, SkeletonData data, IState next) : base(stateMachine)
        {
            _animator = data.Animator;
            _next = next;
        }

        public override void Enter()
        {
            _animator.TakeDamage(() =>
            {
                if (isCurrentState && _stateMachineEnabled)
                    SwitchState(_next.GetType());
            });
        }
    }
}