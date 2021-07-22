using UnityEngine;
using UnityEngine.AI;
using Between.Animations;

namespace Between.Enemies.Skeletons
{
    public class NpcLocomotionController
    {
        public bool HasNpc => _navMeshAgent != null && _animator != null;
        public Vector3 Position => _navMeshAgent.transform.position;
        public float StoppingDistance => _navMeshAgent.stoppingDistance;

        private readonly NavMeshAgent _navMeshAgent;
        private readonly NpcAnimator _animator;

        public NpcLocomotionController(NavMeshAgent navMeshAgent, NpcAnimator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public void Move(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
            UpdateMoveSpeed();
        }

        public void UpdateMoveSpeed()
        {
            _animator.Move(_navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
        }

        public void StartMove()
        {
            _navMeshAgent.isStopped = false;
            _animator.StartMove();
        }
    }
}