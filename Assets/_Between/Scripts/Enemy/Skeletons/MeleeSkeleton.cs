using Between.Enemies.Mavka;
using Between.Interfaces;
using Between.StateMachine;

namespace Between.Enemies.Skeletons
{
    public class MeleeSkeleton : BaseEnemy
    {
        private FinitStateMachine _stateMachine;

        protected override void Start()
        {
            base.Start();

            _stateMachine = new FinitStateMachine();

            var idleState = new IdleDetectionState(_stateMachine, transform, _player, _detectionDistance);
            var moveState = new ChasingState(_stateMachine, _player, _navMeshAgent, _animator, _attackDistance);
            var attackState = new AnimatedAttackState
                (_stateMachine, _animator, _player.GetComponent<IDamagable>(), _damage);
            var cooldownState = new ChasingCooldownState(_stateMachine, _player, _navMeshAgent
                , _animator, _attackDistance, _cooldownTime);
            var takeDamageState = new TakeDamageState(_stateMachine, _animator, idleState);

            _stateMachine.AddStates(idleState, moveState, attackState, cooldownState, takeDamageState);
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        protected override void PerformOnDie()
        {
            _stateMachine.Disable();
            base.PerformOnDie();
        }
    }
}