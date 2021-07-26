using UnityEngine;
using Between.Teams;
using Between.MainCharacter;

namespace Between
{ 
    public class PlayerController : BaseDamagableObject
    {
        public override Team Team => Team.Player;

        private LocomotionController _locomotionController;

        private void Start()
        {
            InitDamagableObject();
            _locomotionController = GetComponent<LocomotionController>();
        }

        public void Push(Vector3 direction, float force)
        {
            if (Mathf.Approximately(force, 0f))
                return;

            _locomotionController.Push(direction, force);
        }

        protected override void PerformOnDie()
        {
            Destroy(gameObject);
        }
    }
}