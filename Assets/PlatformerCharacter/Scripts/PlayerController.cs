using UnityEngine;
using Between.Teams;

namespace Between
{ 
    public class PlayerController : BaseDamagableObject
    {
        public override Team Team => Team.Player;

        public void Heal(float value) => Health = Mathf.Min(Health + value, MaxHealth);

        protected override void PerformOnDie()
        {
            Destroy(gameObject);
        }
    }
}