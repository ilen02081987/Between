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
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _transform;
        private readonly float _attackDistance;

        private readonly WaitForSeconds _updatePathDelay = new WaitForSeconds(.1f);
        private bool _closeToAttack => Vector3.Distance(_target.position, _transform.position) <= _attackDistance;

        public ChasingState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine)
        {
            _target = data.Player.transform;
            _animator = data.Animator;
            _transform = data.Transform;
            _navMeshAgent = data.NavMeshAgent;
        }

        public override void Enter()
        {
            CoroutineLauncher.Start(MoveToTarget());
        }

        public override void Exit()
        {
            CoroutineLauncher.Stop(MoveToTarget());
            _navMeshAgent.isStopped = true;
        }

        private IEnumerator MoveToTarget()
        {
            _navMeshAgent.isStopped = false;
            _animator.StartMove();

            while(!_closeToAttack || !CanAttack)
            {
                Move();
                yield return _updatePathDelay;

                if (_navMeshAgent == null)
                    yield break;
            }

            _navMeshAgent.isStopped = true;

            if (isCurrentState)
                SwitchState(typeof(AnimatedAttackState));
        }

        private void Move()
        {
            _navMeshAgent.SetDestination(_target.position);
            _animator.Move(_navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
        }
    }
}