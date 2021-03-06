using Accord.Diagnostics;
using System;
using System.Collections.Generic;
using TMPro;

namespace Between.StateMachine
{
    public class FinitStateMachine
    {
        public bool Enabled { get; private set; } = true;

        public IState CurrentState { get; private set; }
        private List<IState> _states;

        public void AddStates(IState firstState, params IState[] states)
        {
            CurrentState = firstState;
            _states = new List<IState>();

            _states.Add(firstState);
            _states.AddRange(states);

            EnterState(firstState);
        }

        public void SwitchState(Type stateType)
        {
            UnityEngine.Debug.Log($"Switch state to {stateType.Name}");
            CheckSwitchPossibility(stateType, out IState newState);
            EnterState(newState);
        }

        public void Update() => CurrentState.Update();
        public void Disable()
        {
            CurrentState.Exit();
            Enabled = false;
        }

        public bool CompareState(Type type) => CurrentState.GetType().Equals(type);

        private void EnterState(IState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            state.Enter();
        }

        private void CheckSwitchPossibility(Type stateType, out IState newState)
        {
            if (CurrentState.GetType().Equals(stateType))
                throw new Exception($"[StateMachine] - Can't switch state from {stateType.Name} to {CurrentState.GetType().Name}");

            newState = _states.Find(state => state.GetType().Equals(stateType));

            if (newState == null)
                throw new Exception($"[StateMachine] - Doesn't contains state {stateType.Name}");
        }
    }
}