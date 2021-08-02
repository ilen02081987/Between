using UnityEngine;
using Between.SpellPainting;
using Between.SpellsEffects.Projectile;
using Between.StateMachine;
using Between.Animations;

namespace Between.Enemies.Mavka
{
    public class SingleProjectileCaseState : BaseCastState
    {
        private readonly Transform _target;
        private readonly Transform _spawnPoint;
        private readonly float _castTime;
        private readonly ProjectileSpawner _projectileSpawner;

        public SingleProjectileCaseState(FinitStateMachine stateMachine, NpcAnimator animator, Transform target, Transform spawnPoint, int weight, float castTime)
            : base(stateMachine, animator, weight)
        {
            _target = target;
            _spawnPoint = spawnPoint;
            _castTime = castTime;
            _projectileSpawner = new ProjectileSpawner("MavkaSingleProjectile");
        }

        public override void Enter()
        {
            var spellPainter = new SpellPainter(
                "MavkaSingleProjectile",
                "LineRenderersPainter",
                _spawnPoint.position, _castTime, 0f);

            spellPainter.Complete += () => CompletePaintSpell();
            spellPainter.StartDraw();
            animator.StartWiz();
        }

        private void CompletePaintSpell()
        {
            _projectileSpawner.Spawn(_spawnPoint.position, (_target.position - _spawnPoint.position).normalized);

            if (isCurrentState)
            {
                SwitchState(typeof(CooldownState));
                animator.FinishWiz();
            }
        }
    }
}