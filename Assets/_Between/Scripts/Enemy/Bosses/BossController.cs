using UnityEngine;
using Between.StateMachine;

namespace Between.Enemies.Bosses
{
    public abstract class BossController : BaseEnemy
    {
        [SerializeField] protected Transform[] _severalProjectileSpawnPoints;
        [SerializeField] protected Transform _singleProjectileSpawnPoint;

        private FinitStateMachine _rangeStateMachine;
        private FinitStateMachine _meleeStateMachine;

        protected override void Start()
        {
            base.Start();

            _rangeStateMachine = InitRangeStateMachine();
            _meleeStateMachine = InitMeleeStateMachine();
        }

        private void Update()
        {
            _rangeStateMachine?.Update();
            _meleeStateMachine?.Update();

            TryRotate();
        }

        protected abstract FinitStateMachine InitMeleeStateMachine();
        protected abstract FinitStateMachine InitRangeStateMachine();

        protected override void PerformOnPlayerDie()
        {
            DisableStateMachines();
        }

        protected override void PerformOnDie()
        {
            DisableStateMachines();
            base.PerformOnDie();
        }

        private void DisableStateMachines()
        {
            _rangeStateMachine?.Disable();
            _meleeStateMachine?.Disable();
        }

        private void TryRotate()
        {
            if (player == null)
                return;

            Quaternion rotation = transform.rotation;
            int direction = transform.position.x >= player.Position.x ? -1 : 1;
            var newRotation = Quaternion.Euler(0f, direction * 90f, 0f);

            if (newRotation != rotation)
                transform.rotation = newRotation;
        }
    }
}