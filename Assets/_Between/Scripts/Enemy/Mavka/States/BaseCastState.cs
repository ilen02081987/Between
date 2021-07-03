using Between.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Enemies.Mavka
{
    public abstract class BaseCastState : BaseState
    {
        public abstract int Weight { get; }

        public BaseCastState(FinitStateMachine stateMachine) : base(stateMachine)
        {
        }

        protected void CompleteCast()
        {

        }
    }
}