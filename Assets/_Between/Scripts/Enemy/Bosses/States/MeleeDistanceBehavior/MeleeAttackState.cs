using UnityEngine;
using Between.SpellPainting;
using Between.SpellsEffects.Projectile;
using Between.StateMachine;

namespace Between.Enemies.Mavka
{
    public class MeleeAttackState : BaseState
    {
        private readonly Transform _target;
        private readonly Transform _castPoint;
        private readonly Transform _spawnPoint;
        private readonly ProjectileSpawner _projectileSpawner;

        public MeleeAttackState(FinitStateMachine stateMachine, Transform target, Transform castPoint, Transform spawnPoint) : base(stateMachine) 
        {
            _target = target;
            _castPoint = castPoint;
            _spawnPoint = spawnPoint;
            _projectileSpawner = new ProjectileSpawner("PushableProjectile");
        }

        public override void Enter()
        {
            var spellPainter = new SpellPainter(
                "MavkaMeleeProjectile",
                "TrailPainter",
                _castPoint.position, GameSettings.Instance.MavkaSingleProjectileCastTime, 0f);

            spellPainter.Complete += CompletePaintSpell;
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