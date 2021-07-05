using System.Collections;
using UnityEngine;
using Between.StateMachine;
using Between.Utilities;
using System;

namespace Between.Enemies.Mavka
{
    public class CooldownState : BaseState
    {
        private readonly float _baseValue;
        private readonly float _shiftValue;
        private readonly BaseState _nextState;

        public CooldownState(FinitStateMachine stateMachine, float baseValue, float shiftValue, BaseState nextState) : base(stateMachine) 
        {
            _baseValue = baseValue;
            _shiftValue = shiftValue;
            _nextState = nextState;
        }

        public override void Enter()
        {
            CoroutineLauncher.Start(WaitForCooldown());
        }

        private IEnumerator WaitForCooldown()
        {
            float time = _baseValue + UnityEngine.Random.Range(-_shiftValue, _shiftValue);

            yield return new WaitForSeconds(time);

            if (_isCurrentState)
                SwitchState(_nextState.GetType());
        }
    }
}