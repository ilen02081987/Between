using System;

namespace Between.StateMachine
{
    public abstract class BaseState : IState
    {
        private readonly FinitStateMachine _stateMachine;

        protected bool isCurrentState => _stateMachine.CurrentState.Equals(this);
        protected bool stateMachineEnabled => _stateMachine.Enabled;

        public BaseState(FinitStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }

        protected void SwitchState(Type type) => _stateMachine.SwitchState(type);
    }
}