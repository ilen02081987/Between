using UnityEngine;
using Between.SpellsEffects.ShieldSpell;

namespace Between.Sounds
{
    public class ShieldSoundsController : BaseSoundsController
    {
        [SerializeField] private Shield _shield;
        [SerializeField] private AudioClip _damage;
        [SerializeField, Range(0, 1)] private float _volumeCoefficient;

        private void Start()
        {
            _shield.LivesValueChanged += PlayeDamageSound;
        }

        private void OnDestroy()
        {
            _shield.LivesValueChanged -= PlayeDamageSound;
        }

        private void PlayeDamageSound() => PlayWithoutSource(_damage, _volumeCoefficient);
    }
}