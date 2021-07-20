using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Between.StateMachine;
using Between.Utilities;
using Between.Animations;

namespace Between.Enemies.Skeletons
{
    public class ChasingState : BaseState
    {
        protected virtual bool CanAttack => true;

        private readonly Transform _target;
        private readonly NpcAnimator _animator;
        private readonly Transform _transform;
        private readonly float _attackDistance;
        private readonly NpcLocomotionController _npcLocomotionController;

        private readonly WaitForSeconds _updatePathDelay = new WaitForSeconds(.1f);
        private bool _closeToAttack => Vector3.Distance(_target.position, _transform.position) <= _attackDistance;

        public ChasingState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine)
        {
            _target = data.Player.transform;
            _animator = data.Animator;
            _transform = data.Transform;
            _npcLocomotionController = data.LocomotionController;
        }

        public override void Enter()
        {
            CoroutineLauncher.Start(MoveToTarget());
        }

        public override void Exit()
        {
            CoroutineLauncher.Stop(MoveToTarget());
            _npcLocomotionController.Stop();
        }

        private IEnumerator MoveToTarget()
        {
            _npcLocomotionController.Enable();

            while(!_closeToAttack || !CanAttack)
            {
                _npcLocomotionController.Move(_target.position);
                yield return _updatePathDelay;

                if (!_npcLocomotionController.HasNpc)
                    yield break;
            }

            _npcLocomotionController.Stop();

            if (isCurrentState)
                SwitchState(typeof(AnimatedAttackState));
        }
    }
}