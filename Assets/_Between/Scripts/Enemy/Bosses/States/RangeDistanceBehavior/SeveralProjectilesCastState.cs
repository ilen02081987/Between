using System.Collections;
using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.StateMachine;
using Between.SpellPainting;
using Between.Utilities;
using Between.Animations;

namespace Between.Enemies.Mavka
{
    public class SeveralProjectilesCastState : BaseCastState
    {
        private readonly Transform[] _spawnPoints;
        private readonly Transform _target;
        private readonly float _castTime;
        private readonly float _castDelay;
        private readonly bool _singleCast;
        private readonly ProjectileSpawner _projectileSpawner;

        public SeveralProjectilesCastState(FinitStateMachine stateMachine, NpcAnimator animator, Transform target, int weight,
            float castTime, float castDelay, bool singleCast , params Transform[] spawnPoints) : base(stateMachine, animator, weight)
        {
            _projectileSpawner = new ProjectileSpawner("MavkaSeveralProjectile");
            _spawnPoints = spawnPoints;
            _target = target;
            _castTime = castTime;
            _castDelay = castDelay;
            _singleCast = singleCast;
        }

        public override void Enter()
        {
            CoroutineLauncher.Start(CastProjectiles());
        }

        private IEnumerator CastProjectiles()
        {
            float delay = _castDelay;
            bool singleCast = _singleCast;

            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                if (_spawnPoints[i] == null)
                    yield break;

                int index = i;
                CreateSpellPainter(index, singleCast ? (_spawnPoints.Length - i - 1) * delay : 0f);

                yield return new WaitForSeconds(delay);
            }

            animator.StartWiz();
        }

        private void CreateSpellPainter(int spawnPointIndex, float afterDrawDelay)
        {
            if (_target == null || _spawnPoints[0] == null)
                return;

            var spellPainter = new SpellPainter(
                "MavkaSeveralProjectiles",
                PAINTER_NAME,
                _spawnPoints[spawnPointIndex].position, _castTime, afterDrawDelay);

            spellPainter.Complete += () => CompletePaintSpell(spawnPointIndex);
            spellPainter.StartDraw();
        }

        private void CompletePaintSpell(int spawnPointIndex)
        {
            if (_target == null || _spawnPoints[0] == null)
                return;

            _projectileSpawner.Spawn(
                _spawnPoints[spawnPointIndex].position, 
                (_target.position - _spawnPoints[spawnPointIndex].position).normalized);

            if (isCurrentState && spawnPointIndex == _spawnPoints.Length - 1)
            {
                SwitchState(typeof(CooldownState));
                animator.FinishWiz();
            }
        }
    }
}