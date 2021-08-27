using System;
using UnityEngine;

namespace Between.Sounds
{
    public class SoundTrigger : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _clip;

        private float _defaultSourceVolume;
        private void Start()
        {
            _defaultSourceVolume = _source.volume;
            Volume.OnChanged += ChangeVolume;
        }

        private void OnDestroy()
        {
            Volume.OnChanged -= ChangeVolume;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsPlayer(other))
            {
                _source.clip = _clip;
                _source.Play();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsPlayer(other))
                _source.Stop();
        }

        private bool IsPlayer(Collider other) => other.TryGetComponent<PlayerController>(out var player);
        
        private void ChangeVolume()
        {
            _source.volume = _defaultSourceVolume * Volume.Value;
        }
    }
}