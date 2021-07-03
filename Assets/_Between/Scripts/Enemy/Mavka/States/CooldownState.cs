using System.Collections;
using UnityEngine;
using Between.StateMachine;
using Between.Utilities;

namespace Between.Enemies.Mavka
{
    public class CooldownState : BaseState
    {
        public CooldownState(FinitStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            CoroutineLauncher.Start(WaitForCooldown());
        }

        private IEnumerator WaitForCooldown()
        {
            float shift = GameSettings.Instance.CooldownShift;
            float time = GameSettings.Instance.CooldownBase + Random.Range(-shift, shift);

            yield return new WaitForSeconds(time);

            if (_isCurrentState)
                SwitchState(typeof(IdleDetectionState));
        }
    }
}