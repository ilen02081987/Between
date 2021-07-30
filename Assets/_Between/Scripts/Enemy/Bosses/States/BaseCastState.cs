using Between.Animations;
using Between.StateMachine;

namespace Between.Enemies.Mavka
{
    public abstract class BaseCastState : BaseState
    {
        protected readonly NpcAnimator animator;
        public readonly int Weight;

        public BaseCastState(FinitStateMachine stateMachine, NpcAnimator animator, int weight) : base(stateMachine)
        {
            this.animator = animator;
            Weight = weight;
        }
    }
}