using System.Collections;
using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.StateMachine;
using Between.Utilities;

namespace Between.Enemies.Mavka
{
    public class TestCastState : BaseCastState
    {
        public override int Weight => 1;

        private readonly Transform _spawnPoint;
        private readonly Transform _target;
        
        private readonly ProjectileSpawner _projectileSpawner;
        private readonly float _delay = 1f;

        public TestCastState(FinitStateMachine stateMachine, Transform spawnPoint, Transform target) : base(stateMachine)
        {
            _projectileSpawner = new ProjectileSpawner("MavkaSingleProjectile", 0f);
            _spawnPoint = spawnPoint;
            _target = target;
        }

        public override void Enter()
        {
            CoroutineLauncher.Start(CastProjectile());
        }

        private IEnumerator CastProjectile()
        {
            yield return new WaitForSeconds(_delay);

            _projectileSpawner.Spawn(_spawnPoint.position, (_target.position - _spawnPoint.position).normalized);

            if (_isCurrentState)
                SwitchState(typeof(CooldownState));
        }
    }
}