using UnityEngine;

namespace Between.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class BaseSoundsController : MonoBehaviour
    {
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.volume *= MainVolume.Value;
        }

        public void Play(AudioClip clip)
        {
            if (clip != null)
                _source.PlayOneShot(clip);
        }
    }
}