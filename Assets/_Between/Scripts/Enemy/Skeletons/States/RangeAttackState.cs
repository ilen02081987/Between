using UnityEngine;
using Between.Animations;
using Between.SpellsEffects.Projectile;
using Between.StateMachine;

namespace Between.Enemies.Skeletons
{
    public class RangeAttackState : BaseState
    {
        private readonly NpcAnimator _animator;
        private readonly BaseDamagableObject _target;
        private readonly Transform _spawnPoint;
        private readonly ProjectileSpawner _projectileSpawner;

        public RangeAttackState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine)
        {
            _animator = data.Animator;
            _target = data.Player;
            _spawnPoint = data.ArrowSpawnPoint;
            _projectileSpawner = new ProjectileSpawner(data.ArrowPrefab.name, 0f);
        }

        public override void Enter()
        {
            _animator.Attack(() =>
            {
                SpawnProjectile();
                SwitchState();
            });
        }

        private void SpawnProjectile()
        {
            _projectileSpawner.Spawn(_spawnPoint.position, (_target.Position - _spawnPoint.position).normalized);
        }

        private void SwitchState()
        {
            SwitchState(typeof(ChasingCooldownState));
        }
    }
}