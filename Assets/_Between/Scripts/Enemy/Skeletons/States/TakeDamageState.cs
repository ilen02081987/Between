using Between.Animations;
using Between.Enemies.Skeletons;
using Between.StateMachine;

namespace Between.Enemies
{
    public class TakeDamageState : BaseState
    {
        private readonly NpcLocomotionController _locomotionController;
        private readonly NpcAnimator _animator;
        private readonly IState _next;

        public TakeDamageState(FinitStateMachine stateMachine, SkeletonData data, IState next) : base(stateMachine)
        {
            _locomotionController = data.LocomotionController;
            _animator = data.Animator;
            _next = next;
        }

        public override void Enter()
        {
            _locomotionController.Stop();

            _animator.TakeDamage(() =>
            {
                if (isCurrentState && _stateMachineEnabled)
                    SwitchState(_next.GetType());
            });
        }
    }
}