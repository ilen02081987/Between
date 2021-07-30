using System.Collections;
using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.StateMachine;
using Between.SpellPainting;
using Between.Utilities;

namespace Between.Enemies.Mavka
{
    public class SeveralProjectilesCastState : BaseCastState
    {
        public override int Weight => GameSettings.Instance.SeveralProjectilesCastWeight;

        private readonly Transform[] _spawnPoints;
        private readonly Transform _target;
        private readonly ProjectileSpawner _projectileSpawner;

        public SeveralProjectilesCastState(FinitStateMachine stateMachine, Transform target, params Transform[] spawnPoints) : base(stateMachine)
        {
            _projectileSpawner = new ProjectileSpawner("MavkaSeveralProjectile", 0f);
            _spawnPoints = spawnPoints;
            _target = target;
        }

        public override void Enter()
        {
            CoroutineLauncher.Start(CastProjectiles());
        }

        private IEnumerator CastProjectiles()
        {
            var delay = GameSettings.Instance.SeveralProjectilesCastDelay;
            var singleCast = GameSettings.Instance.SeveralProjectilesSingleCast;

            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                int index = i;
                CreateSpellPainter(index, singleCast ? (_spawnPoints.Length - i - 1) * delay : 0f);

                yield return new WaitForSeconds(delay);
            }
        }

        private void CreateSpellPainter(int spawnPointIndex, float afterDrawDelay)
        {
            var spellPainter = new SpellPainter(
                "MavkaSeveralProjectiles",
                "LineRenderersPainter",
                _spawnPoints[spawnPointIndex].position, GameSettings.Instance.SeveralProjectilesCastTime
                , afterDrawDelay);

            spellPainter.Complete += () => CompletePaintSpell(spawnPointIndex);
            spellPainter.StartDraw();
        }

        private void CompletePaintSpell(int spawnPointIndex)
        {
            if (_target == null)
                return;

            _projectileSpawner.Spawn(
                _spawnPoints[spawnPointIndex].position, 
                (_target.position - _spawnPoints[spawnPointIndex].position).normalized);
            
            if (isCurrentState && spawnPointIndex == _spawnPoints.Length - 1)
                SwitchState(typeof(CooldownState));
        }
    }
}