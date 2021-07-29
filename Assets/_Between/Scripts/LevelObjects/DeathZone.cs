using UnityEngine;
using Between.Damage;

namespace Between.LevelObjects
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BaseDamagableObject>(out var damagable))
                damagable.ApplyDamage(new DamageItem(DamageType.Projectile, float.MaxValue));
        }
    }
}