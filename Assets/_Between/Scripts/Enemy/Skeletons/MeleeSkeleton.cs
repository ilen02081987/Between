using UnityEngine;
using Between.SpellsEffects.ShieldSpell;
using Between.StateMachine;

namespace Between.Enemies.Skeletons
{
    public class MeleeSkeleton : BaseSkeleton
    {
        [SerializeField] private ShieldTrigger _playerShieldTrigger;

        private bool _isDestroingShield => stateMachine.CompareState(typeof(DestroyShieldState));

        protected override void InitStateMachine()
        {
            stateMachine = new FinitStateMachine();

            BaseState idleState = null;

            if (data.WayPoints != null && data.WayPoints.Length > 1)
                idleState = new PatrolState(stateMachine, data);
            else
                idleState = new IdleDetectionState(stateMachine, data);

            var attackState = new MeleeAttackState(stateMachine, data);
            var chasingState = new ChasingState(stateMachine, data, attackState);
            var cooldownState = new ChasingCooldownState(stateMachine, data, attackState);
            var takeDamageState = new TakeDamageState(stateMachine, data, chasingState);
            var destroyShieldState = new DestroyShieldState(stateMachine, data);

            stateMachine.AddStates(idleState, chasingState, attackState, cooldownState, takeDamageState, destroyShieldState);
        }

        protected override void InitNpc()
        {
            _playerShieldTrigger.OnCollideWithShield += TryStartDestroyShield;
        }

        private void TryStartDestroyShield(Shield shield)
        {
            if (!_isDestroingShield)
            {
                data.Shield = shield;
                stateMachine.SwitchState(typeof(DestroyShieldState));
            }
        }
    }
}