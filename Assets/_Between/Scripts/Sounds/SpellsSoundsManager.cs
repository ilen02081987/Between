using System.IO;
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

        private void PlayNoRecognizeSound() => _source.PlayOneShot(_noRecognize);
        private void PlayNoEnoughSound() => _source.PlayOneShot(_noMana);

        public void Dispose()
        {
            BaseSpell.NotEnoughMana += PlayNoEnoughSound;
            BaseSpell.NotRecognizeSpell += PlayNoRecognizeSound;
        }
    }
}