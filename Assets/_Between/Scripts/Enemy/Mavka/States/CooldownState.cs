using System.Collections;
using UnityEngine;
using Between.StateMachine;
using Between.Utilities;

namespace Between.Enemies.Mavka
{
    public class CooldownState : BaseState
    {
        private readonly float _baseValue;
        private readonly float _shiftValue;
        private readonly BaseState _nextState;

        public CooldownState(FinitStateMachine stateMachine, BaseState nextState, float baseValue, float shiftValue = 0f)
            : base(stateMachine) 
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
            float time = _baseValue + Random.Range(-_shiftValue, _shiftValue);

            yield return new WaitForSeconds(time);

            if (IsCurrentState)
                SwitchState(_nextState.GetType());
        }
    }
}