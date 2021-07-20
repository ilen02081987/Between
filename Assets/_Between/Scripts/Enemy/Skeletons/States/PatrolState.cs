using System.Collections;
using UnityEngine;
using Between.StateMachine;
using Between.Utilities;

namespace Between.Enemies.Skeletons
{
    public class PatrolState : BaseState
    {
        private readonly NpcLocomotionController _locomotionController;
        private readonly Transform[] _wayPoints;
        private readonly Transform _player;
        private readonly Transform _owner;
        private readonly float _detectionDistance;

        private readonly float _waypointReachTreshhold = .5f;

        private readonly WaitForSeconds _updatePathDelay = new WaitForSeconds(.1f);

        private bool _seePlayer => Vector3.Distance(_owner.position, _player.position) <= _detectionDistance;

        public PatrolState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine)
        {
            _locomotionController = data.LocomotionController;
            _wayPoints = data.WayPoints;
            _player = data.Player.transform;
            _owner = data.Transform;
            _detectionDistance = data.DetectionDistance;
        }

        public override void Enter()
        {
            CoroutineLauncher.Start(Patrol());
        }

        public override void Exit()
        {
            _locomotionController.Stop();
            CoroutineLauncher.Stop(Patrol());
        }

        private IEnumerator Patrol()
        {
            _locomotionController.Enable();

            int waypointIndex = 0;
            _locomotionController.Move(_wayPoints[waypointIndex].position);

            while (isCurrentState)
            {
                if (_seePlayer)
                    SwitchState(typeof(ChasingState));

                if (ReachWaypoint(waypointIndex))
                {
                    waypointIndex = (waypointIndex + 1) % (_wayPoints.Length);
                    _locomotionController.Move(_wayPoints[waypointIndex].position);
                }

                _locomotionController.UpdateMoveSpeed();
                yield return _updatePathDelay;
            }
        }

        private bool ReachWaypoint(int waypointIndex)
        {
            var distance = Vector3.Distance(_locomotionController.Position, _wayPoints[waypointIndex].position);
             return distance <= _locomotionController.StoppingDistance + _waypointReachTreshhold;
        }
    }
}