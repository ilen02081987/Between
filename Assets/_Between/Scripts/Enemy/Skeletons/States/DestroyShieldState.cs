using System.Collections;
using UnityEngine;
using Between.Animations;
using Between.Damage;
using Between.SpellsEffects.ShieldSpell;
using Between.StateMachine;
using RH.Utilities.Coroutines;

namespace Between.Enemies.Skeletons
{
    public class DestroyShieldState : BaseState
    {
        private readonly NpcAnimator _animator;
        private readonly DamageItem _damage;
        private readonly WaitForSeconds _attackCooldown;
        private readonly SkeletonData _data;

        private Shield _shield;

        public DestroyShieldState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine)
        {
            _animator = data.Animator;
            _damage = data.DamageItem;
            _attackCooldown = new WaitForSeconds(data.CooldownTime);
            _data = data;
        }

        public override void Enter()
        {
            _shield = _data.Shield;

            if (_shield != null)
                CoroutineLauncher.Start(DestroyShield());
            else
                SwitchState(typeof(ChasingState));
        }

        public override void Exit()
        {
            CoroutineLauncher.Stop(DestroyShield());
        }

        private IEnumerator DestroyShield()
        {
            while(_shield != null)
            {
                _animator.Attack(() => _shield.ApplyDamage(_damage));
                yield return _attackCooldown;
            }

            if (isCurrentState)
                SwitchState(typeof(ChasingState));
        }
    }
}