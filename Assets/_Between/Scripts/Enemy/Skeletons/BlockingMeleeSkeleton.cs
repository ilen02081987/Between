using UnityEngine;
using Between.ShieldsSpawning;

namespace Between.Enemies.Skeletons
{
    public class BlockingMeleeSkeleton : MeleeSkeleton
    {
        [SerializeField] private ProjectileTrigger[] _triggers;

        protected override void InitStateMachine()
        {
            base.InitStateMachine();

            var blockingState = new BlockingState(stateMachine, this, data);
            stateMachine.AddState(blockingState);
        }

        protected override void InitNpc()
        {
            foreach (var trigger in _triggers)
                trigger.OnDetect += Defend;
        }

        protected override void PerformOnDie()
        {
            foreach (var trigger in _triggers)
                trigger.OnDetect -= Defend;

            base.PerformOnDie();
        }

        private void Defend()
        {
            if (this != null && !stateMachine.CompareState(typeof(BlockingState)))
                stateMachine.SwitchState(typeof(BlockingState));
        }
    }
}