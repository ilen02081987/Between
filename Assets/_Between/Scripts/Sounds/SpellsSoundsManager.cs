using UnityEngine;
using Between.Controllers;
using Between.Spells;

namespace Between.Sounds
{
    public class SpellsSoundsManager : MonoBehaviour, IController
    {
        [SerializeField] private AudioSource _source;

        [SerializeField] private AudioClip _noMana;
        [SerializeField] private AudioClip _noRecognize;

        public void Init()
        {
            BaseSpell.NotEnoughMana += PlayNoEnoughSound;
            BaseSpell.NotRecognizeSpell += PlayNoRecognizeSound;
        }
        public void Dispose()
        {
            BaseSpell.NotEnoughMana += PlayNoEnoughSound;
            BaseSpell.NotRecognizeSpell += PlayNoRecognizeSound;
        }
        private void PlayNoRecognizeSound() => PlayOneShot(_noRecognize);
        private void PlayNoEnoughSound() => PlayOneShot(_noMana);

        private void PlayOneShot(AudioClip clip)
        {
            if (_source == null || clip == null)
                return;

            _source.PlayOneShot(clip);
        }
    }
}