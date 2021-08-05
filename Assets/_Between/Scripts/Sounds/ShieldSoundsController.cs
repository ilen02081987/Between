using Between.SpellsEffects.ShieldSpell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Sounds
{
    public class ShieldSoundsController : BaseSoundsController
    {
        [SerializeField] private Shield _shield;
        [SerializeField] private AudioClip _damage;

        private void Start()
        {
            _shield.LivesValueChanged += PlayeDamageSound;
        }

        private void OnDestroy()
        {
            _shield.LivesValueChanged -= PlayeDamageSound;
        }

        private void PlayeDamageSound() => Play(_damage);
    }
}