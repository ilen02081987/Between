using Between.Animations;
using Between.Damage;
using Between.Sounds;
using Between.StateMachine;
using UnityEngine;

namespace Between.Enemies.Skeletons
{
    public class MeleeAttackState : BaseState
    {
        private readonly NpcAnimator _animator;
        private readonly BaseDamagableObject _target;
        private readonly DamageItem _damage;
        private readonly float _attackDistance;
        private readonly AudioClip _attack;
        private readonly AudioClip _hit;

        public MeleeAttackState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine)
        {
            _animator = data.Animator;
            _target = data.Player;
            _damage = data.DamageItem;
            _attackDistance = data.AttackDistance;
            _attack = data.AttackSound;
            _hit = data.HitSound;
        }

        public override void Enter()
        {
            PlayAttackSound();

            _animator.Attack(() =>
            {
                ApplyDamageToPlayer();
                SwitchState();
            });
        }

        private void PlayAttackSound()
        {
            if (_attack != null)
                AudioSource.PlayClipAtPoint(_attack, _animator.transform.position, Volume.Value);
        }

        private void ApplyDamageToPlayer()
        {
            if (_target == null || _animator == null)
                return;

            if (Vector3.Distance(_target.Position, _animator.Position) <= _attackDistance)
            {
                _target.ApplyDamage(_damage);
                PlayHitSound();
            }
        }

        private void SwitchState()
        {
            SwitchState(typeof(ChasingCooldownState));
        }

        private void PlayHitSound()
        {
            if (_hit != null)
                AudioSource.PlayClipAtPoint(_hit, _animator.transform.position, Volume.Value);
        }
    }
}