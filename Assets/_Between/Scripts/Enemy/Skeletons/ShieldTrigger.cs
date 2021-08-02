using System;
using UnityEngine;
using Between.SpellsEffects.ShieldSpell;

namespace Between.Enemies.Skeletons
{
    [RequireComponent(typeof(Collider))]
    public class ShieldTrigger : MonoBehaviour
    {
        public event Action<Shield> OnCollideWithShield;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Shield>(out var shield))
                OnCollideWithShield?.Invoke(shield);
        }
    }
}