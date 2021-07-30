using UnityEngine;
using Between.SpellsEffects.ShieldSpell;
using Between.StateMachine;

namespace Between.Enemies.Skeletons
{
    public class MeleeSkeleton : BaseSkeleton
    {
        protected override void InitStateMachine()
        {
            _stateMachine = new FinitStateMachine();

            BaseState idleState = null;

            if (data.WayPoints != null && data.WayPoints.Length > 1)
                idleState = new PatrolState(_stateMachine, data);
            else
                idleState = new IdleDetectionState(_stateMachine, data);

            var attackState = new MeleeAttackState(_stateMachine, data);
            var chasingState = new ChasingState(_stateMachine, data, attackState);
            var cooldownState = new ChasingCooldownState(_stateMachine, data, attackState);
            var takeDamageState = new TakeDamageState(_stateMachine, data, chasingState);
            var destroyShieldState = new DestroyShieldState(_stateMachine, data);

            _stateMachine.AddStates(idleState, chasingState, attackState, cooldownState, takeDamageState, destroyShieldState);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Shield>(out var shield) && !_isDestroingShield)
            {
                data.Shield = shield;
                _stateMachine.SwitchState(typeof(DestroyShieldState));
            }
        }
    }
}