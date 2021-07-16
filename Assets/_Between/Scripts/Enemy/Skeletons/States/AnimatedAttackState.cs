using Between.Animations;
using Between.Damage;
using Between.Interfaces;
using Between.StateMachine;
using UnityEngine;

namespace Between.Enemies.Skeletons
{
    public class AnimatedAttackState : BaseState
    {
        private readonly NpcAnimator _animator;
        private readonly BaseDamagableObject _target;
        private readonly DamageItem _damage;
        private readonly float _attackDistance;

        public AnimatedAttackState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine)
        {
            _animator = data.Animator;
            _target = data.Player;
            _damage = data.DamageItem;
            _attackDistance = data.AttackDistance;
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
            if (Vector3.Distance(_target.Position, _animator.Position) <= _attackDistance)
                _target.ApplyDamage(_damage);
        }

        private void SwitchState()
        {
            SwitchState(typeof(ChasingCooldownState));
        }
    }
}