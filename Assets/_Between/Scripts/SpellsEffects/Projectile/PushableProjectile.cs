using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    public class PushableProjectile : Projectile
    {
        protected override void ApplySpecialEffects(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<PlayerController>(out var controller))
            {
                //Push player
            }
        }
    }
}