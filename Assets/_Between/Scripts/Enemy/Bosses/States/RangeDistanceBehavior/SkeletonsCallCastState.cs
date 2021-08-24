using UnityEngine;
using Between.StateMachine;
using Between.Animations;
using Between.SpellPainting;
using System.Collections;
using Between.Utilities;

namespace Between.Enemies.Mavka
{
    public class SkeletonsCallCastState : BaseCastState
    {
        private readonly Vector3[] _spawnPoints;
        private readonly float _castDelay;
        private readonly float _castTime;
        private readonly bool _singleCast;
        private readonly BaseEnemy _enemy;

        public SkeletonsCallCastState(FinitStateMachine stateMachine, NpcAnimator animator, int weight, float castDelay, float castTime, bool singleCast, BaseEnemy enemy, params Transform[] spawnPoints) : base(stateMachine, animator, weight)
        {
            _castDelay = castDelay;
            _castTime = castTime;
            _singleCast = singleCast;
            _enemy = enemy;

            _spawnPoints = new Vector3[spawnPoints.Length];
            for (int i = 0; i < spawnPoints.Length; i++)
                _spawnPoints[i] = spawnPoints[i].position;
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
            var spellPainter = new SpellPainter(
                "LichSkeletonsCast",
                PAINTER_NAME,
                _spawnPoints[spawnPointIndex], _castTime, afterDrawDelay);

            spellPainter.Complete += () => CompletePaintSpell(spawnPointIndex);
            spellPainter.StartDraw();
        }

        private void CompletePaintSpell(int spawnPointIndex)
        {
            MonoBehaviour.Instantiate(_enemy, _spawnPoints[spawnPointIndex], Quaternion.identity);
            
            if (isCurrentState && spawnPointIndex == _spawnPoints.Length - 1)
            {
                SwitchState(typeof(CooldownState));
                animator.FinishWiz();
            }
        }
    }
}