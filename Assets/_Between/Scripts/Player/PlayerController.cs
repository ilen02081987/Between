using UnityEngine;
using Between.Teams;

namespace Between
{ 
    public class PlayerController : BaseDamagableObject
    {
        public override Team Team => Team.Player;

        protected override void PerformOnDie()
        {
            Destroy(gameObject);
        }
    }
}