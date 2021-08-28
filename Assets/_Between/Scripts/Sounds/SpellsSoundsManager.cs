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

        private float _defaultVolume;

        public void Init()
        {
            _defaultVolume = _source.volume;

            BaseSpell.NotEnoughMana += PlayNoEnoughSound;
            BaseSpell.NotRecognizeSpell += PlayNoRecognizeSound;
            Volume.OnChanged += ChangeVolume;
        }

        private void ChangeVolume()
        {
            if (_source == null)
            {
                Volume.OnChanged -= ChangeVolume;
                Destroy(this);
                return;
            }

            _source.volume = Volume.Value * _defaultVolume;
        }

        public void Dispose()
        {
            Volume.OnChanged -= ChangeVolume;
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