using System.Collections;
using UnityEngine;
using Between.Animations;
using Between.Damage;
using Between.SpellsEffects.ShieldSpell;
using Between.StateMachine;
using Between.Utilities;

namespace Between.Enemies.Skeletons
{
    public class DestroyShieldState : BaseState
    {
        private readonly NpcAnimator _animator;
        private readonly DamageItem _damage;
        private readonly WaitForSeconds _attackCooldown;

        private readonly Vector3 _point0;
        private readonly Vector3 _point1;
        private readonly float _radius;

        private Shield _shield;

        public DestroyShieldState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine)
        {
            _animator = data.Animator;
            _damage = data.DamageItem;
            _attackCooldown = new WaitForSeconds(data.CooldownTime);

            _point0 = data.Collider.center + Vector3.up * data.Collider.height / 2f;
            _point1 = data.Collider.center - Vector3.up * data.Collider.height / 2f;
            _radius = data.Collider.radius;
        }

        public override void Enter()
        {
            FindShield();

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
                FindShield();
            }

            if (isCurrentState)
                SwitchState(typeof(ChasingState));
        }

        private void FindShield()
        {
            var colliders = Physics.OverlapCapsule(_point0, _point1, _radius);

            if (colliders == null || colliders.Length == 0)
                return;

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<Shield>(out var shield))
                {
                    _shield = shield;
                    return;
                }
            }
        }
    }
}