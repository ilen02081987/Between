using Between.StateMachine;

namespace Between.Enemies.Skeletons
{
    public class BlockingState : BaseState
    {
        private readonly NpcLocomotionController _npcController;
        private readonly BaseDamagableObject _owner;

        public BlockingState(FinitStateMachine stateMachine, BaseDamagableObject owner, SkeletonData data) : base(stateMachine)
        {
            _npcController = data.LocomotionController;
            _owner = owner;
        }

        public override void Enter()
        {
            _owner.Immortal = true;
            _npcController.Defend(() =>
            {
                _owner.Immortal = false;
                SwitchState(typeof(ChasingState));
            });
        }
    }
}