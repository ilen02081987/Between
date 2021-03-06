using UnityEngine;
using Between.SpellPainting;
using Between.SpellsEffects.Projectile;
using Between.StateMachine;

namespace Between.Enemies.Mavka
{
    public class SingleProjectileCaseState : BaseCastState
    {
        public override int Weight => GameSettings.Instance.SingleProjectilesCastWeight;

        private readonly Transform _target;
        private readonly Transform _spawnPoint;
        private readonly ProjectileSpawner _projectileSpawner;

        public SingleProjectileCaseState(FinitStateMachine stateMachine, Transform target, Transform spawnPoint)
            : base(stateMachine)
        {
            _target = target;
            _spawnPoint = spawnPoint;

            _projectileSpawner = new ProjectileSpawner("MavkaSingleProjectile", 0f);
        }

        public override void Enter()
        {
            var spellPainter = new SpellPainter(
                "MavkaSingleProjectile",
                "LineRenderersPainter",
                _spawnPoint.position, GameSettings.Instance.MavkaSingleProjectileCastTime, 0f);

            spellPainter.Complete += () => CompletePaintSpell();
            spellPainter.StartDraw();
        }

        private void CompletePaintSpell()
        {
            _projectileSpawner.Spawn(_spawnPoint.position, (_target.position - _spawnPoint.position).normalized);

            if (isCurrentState)
                SwitchState(typeof(CooldownState));
        }
    }
}