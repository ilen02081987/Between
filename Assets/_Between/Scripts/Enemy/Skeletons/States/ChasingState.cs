using System.Collections;
using UnityEngine;
using Between.StateMachine;
using Between.Utilities;

namespace Between.Enemies.Skeletons
{
    public class ChasingState : BaseState
    {
        protected virtual bool CanAttack => true;

        private readonly Transform _target;
        private readonly Transform _transform;
        private readonly float _attackDistance;
        private readonly NpcLocomotionController _npcLocomotionController;
        private readonly WaitForSeconds _updatePathDelay = new WaitForSeconds(.1f);
        private readonly IState _attackState;

        private bool _closeEnough => Vector3.Distance(_target.position, _transform.position) <= _attackDistance;

        public ChasingState(FinitStateMachine stateMachine, SkeletonData data, IState attackState) : base(stateMachine)
        {
            _target = data.Player.transform;
            _transform = data.Transform;
            _attackDistance = data.AttackDistance;
            _npcLocomotionController = data.LocomotionController;
            _attackState = attackState;
        }

        public override void Update()
        {
            if (!_closeEnough)
            {
                _npcLocomotionController.StartMove();
                _npcLocomotionController.Move(_target.position);
            }
            else 
            {
                _npcLocomotionController.Stop();
                
                if (CanAttack)
                    SwitchState(_attackState.GetType());
            }
        }

        public override void Exit()
        {
            _npcLocomotionController.Stop();
        }
    }
}