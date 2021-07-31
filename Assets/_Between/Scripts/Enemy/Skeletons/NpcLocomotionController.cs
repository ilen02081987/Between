using UnityEngine;
using UnityEngine.AI;
using Between.Animations;
using System;

namespace Between.Enemies.Skeletons
{
    public class NpcLocomotionController
    {
        public bool HasNpc => _navMeshAgent != null && _animator != null;
        public Vector3 Position => transform.position;
        public float StoppingDistance => _navMeshAgent.stoppingDistance;

        private readonly NavMeshAgent _navMeshAgent;
        private readonly NpcAnimator _animator;

        private Transform transform => _navMeshAgent.transform;

        public NpcLocomotionController(NavMeshAgent navMeshAgent, NpcAnimator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public void Defend(Action onComplete) => _animator.Defend(onComplete);

        public void Move(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            UpdateMoveSpeed();
        }

        public void UpdateMoveSpeed()
        {
            _animator.Move(_navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
        }

        public void Stop()
        {
            if (_navMeshAgent != null)
                _navMeshAgent.isStopped = true;
        }

        public void StartMove()
        {
            _navMeshAgent.isStopped = false;
            _animator.StartMove();
        }

        public void RotateTo(Transform target)
        {
            var targetPositionX = target.position.x;
            float angle = targetPositionX >= Position.x ? 90 : -90;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        public void Attack(Action onAnimationAttack) => _animator.Attack(onAnimationAttack);
    }
}