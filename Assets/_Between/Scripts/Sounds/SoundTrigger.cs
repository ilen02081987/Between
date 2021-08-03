using UnityEngine;

namespace Between.Sounds
{
    public class SoundTrigger : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _clip;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
                _source.PlayOneShot(_clip);
        }
    }
}