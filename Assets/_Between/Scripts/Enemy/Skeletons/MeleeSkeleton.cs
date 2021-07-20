using Between.Enemies.Mavka;
using Between.Interfaces;
using Between.SpellsEffects.ShieldSpell;
using Between.StateMachine;
using UnityEngine;

namespace Between.Enemies.Skeletons
{
    public class MeleeSkeleton : BaseSkeleton
    {
        private FinitStateMachine _stateMachine;

        private bool _isTakeDamage => _stateMachine.CompareState(typeof(TakeDamageState));
        private bool _isDestroingShield => _stateMachine.CompareState(typeof(DestroyShieldState));

        protected override void Start()
        {
            base.Start();
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            _stateMachine = new FinitStateMachine();

            BaseState idleState = null;

            if (data.WayPoints != null && data.WayPoints.Length > 1)
                idleState = new PatrolState(_stateMachine, data);
            else
                idleState = new IdleDetectionState(_stateMachine, data);

            var chasingState = new ChasingState(_stateMachine, data);
            var attackState = new AnimatedAttackState(_stateMachine, data);
            var cooldownState = new ChasingCooldownState(_stateMachine, data);
            var takeDamageState = new TakeDamageState(_stateMachine, data, chasingState);
            var destroyShieldState = new DestroyShieldState(_stateMachine, data);

            _stateMachine.AddStates(idleState, chasingState, attackState, cooldownState, takeDamageState, destroyShieldState);
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Shield>(out var shield) && !_isDestroingShield)
            {
                data.Shield = shield;
                _stateMachine.SwitchState(typeof(DestroyShieldState));
            }
        }

        protected override void PerformOnDamage()
        {
            if (!_isTakeDamage)
                _stateMachine.SwitchState(typeof(TakeDamageState));
        }

        protected override void PerformOnDie()
        {
            _stateMachine.Disable();
            base.PerformOnDie();
        }
    }
}