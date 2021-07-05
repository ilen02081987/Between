using UnityEngine;
using Between.StateMachine;

namespace Between.Enemies.Mavka
{
    public class AttackState : BaseState
    {
        private readonly Transform _owner;
        private readonly Transform _target;

        private readonly BaseCastState[] _castintStates;

        public AttackState(FinitStateMachine stateMachine, Transform owner, Transform target
            , params BaseCastState[] castingStates) : base(stateMachine)
        {
            _owner = owner;
            _target = target;
            _castintStates = castingStates;
        }

        public override void Enter()
        {
            float spellWeight = Random.Range(0f, CountSpellsWeights());
            Debug.Log(spellWeight);
            int currentWeight = 0;
            BaseCastState currentState = null;

            foreach (var state in _castintStates)
            {
                currentWeight += state.Weight;

                if (spellWeight <= currentWeight)
                {
                    currentState = state;
                    break;
                }
            }

            SwitchState(currentState.GetType());
        }

        private float CountSpellsWeights()
        {
            float totalWeights = default;

            foreach (var state in _castintStates)
                totalWeights += state.Weight;

            return totalWeights;
        }
    }
}