using UnityEngine;
using Between.StateMachine;
using Accord.Collections;

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
            int spellWeight = Random.Range(0, CountSpellsWeights());
            int currentWeight = 0;
            BaseCastState currentState = null;

            foreach (var state in _castintStates)
            {
                currentWeight += state.Weight;

                if (spellWeight < currentWeight)
                    currentState = state;
            }

            SwitchState(currentState.GetType());
        }

        private int CountSpellsWeights()
        {
            int totalWeights = default;

            foreach (var state in _castintStates)
                totalWeights += state.Weight;

            return totalWeights;
        }
    }
}