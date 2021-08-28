using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

namespace Between.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class BaseSoundsController : MonoBehaviour
    {
        private AudioSource _source;
        private float _defaultSourceVolume;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _defaultSourceVolume = _source.volume;
            _source.volume *= Volume.Value;

            Volume.OnChanged += ChangeVolume;
        }

        private void OnDestroy()
        {
            Volume.OnChanged -= ChangeVolume;
        }

        private void ChangeVolume()
        {
            if (this == null)
                return;

            _source.volume = Volume.Value * _defaultSourceVolume;
        }

        public void Play(AudioClip clip)
        {
            if (clip != null)
                _source.PlayOneShot(clip);
        }

        protected void PlayWithoutSource(AudioClip clip, float volumeCoeff = 1f)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, Volume.Value * volumeCoeff);
        }
    }
}