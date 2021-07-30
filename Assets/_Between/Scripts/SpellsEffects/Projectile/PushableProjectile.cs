using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    public class PushableProjectile : Projectile
    {
        [SerializeField] private float _pushForce;

        protected override void ApplySpecialEffects(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<PlayerController>(out var controller))
            {
                Vector3 direction = (controller.transform.position - transform.position).normalized;
                controller.Push(direction, _pushForce);
            }
        }
    }
}