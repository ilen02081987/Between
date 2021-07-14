using Between.Animations;
using Between.Damage;
using Between.Interfaces;
using Between.StateMachine;

namespace Between.Enemies.Skeletons
{
    public class AnimatedAttackState : BaseState
    {
        private readonly NpcAnimator _animator;
        private readonly IDamagable _target;
        private readonly DamageItem _damage;

        public AnimatedAttackState(FinitStateMachine stateMachine, NpcAnimator animator, 
            IDamagable target, DamageItem damage) : base(stateMachine)
        {
            _animator = animator;
            _target = target;
            _damage = damage;
        }

        public override void Enter()
        {
            _animator.Attack(() =>
            {
                ApplyDamageToPlayer();
                SwitchState();
            });
        }

        private void ApplyDamageToPlayer()
        {
            _target.ApplyDamage(_damage);
        }

        private void SwitchState()
        {
            SwitchState(typeof(ChasingCooldownState));
        }
    }
}