using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

namespace Between.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class BaseSoundsController : MonoBehaviour
    {
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.volume *= Volume.Value;

            Volume.OnValueChanged += ChangeVolume;
        }

        private void OnDestroy()
        {
            Volume.OnValueChanged -= ChangeVolume;
        }

        private void ChangeVolume()
        {
            if (this == null)
                return;

            _source.volume = Volume.Value;
        }

        public void Play(AudioClip clip)
        {
            if (clip != null)
                _source.PlayOneShot(clip);
        }
    }
}