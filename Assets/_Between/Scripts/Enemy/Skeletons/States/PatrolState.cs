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

        private int _wayPointIndex = 0;

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
            _wayPointIndex = 0;

            _locomotionController.StartMove();
            _locomotionController.Move(_wayPoints[_wayPointIndex].position);
        }

        public override void Update()
        {
            if (_player == null)
                return;

            if (isCurrentState)
            {
                if (_seePlayer)
                    SwitchState(typeof(ChasingState));

                if (ReachWaypoint(_wayPointIndex))
                {
                    _wayPointIndex = (_wayPointIndex + 1) % (_wayPoints.Length);
                    _locomotionController.Move(_wayPoints[_wayPointIndex].position);
                }

                _locomotionController.UpdateMoveSpeed();
            }
        }

        public override void Exit()
        {
            _locomotionController.Stop();
        }

        private bool ReachWaypoint(int waypointIndex)
        {
            var distance = Vector3.Distance(_locomotionController.Position, _wayPoints[waypointIndex].position);
             return distance <= _locomotionController.StoppingDistance + _waypointReachTreshhold;
        }
    }
}