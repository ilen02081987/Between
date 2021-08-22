using Between.Animations;
using Between.StateMachine;

namespace Between.Enemies.Mavka
{
    public abstract class BaseCastState : BaseState
    {
        public const string PAINTER_NAME = "TrailPainter";

        protected readonly NpcAnimator animator;
        public readonly int Weight;

        public BaseCastState(FinitStateMachine stateMachine, NpcAnimator animator, int weight) : base(stateMachine)
        {
            this.animator = animator;
            Weight = weight;
        }
    }
}