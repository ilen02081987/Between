using Between.MainCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Between.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSoundsController : MonoBehaviour
    {
        [SerializeField] private LocomotionController _locomotionController;

        [Space]
        [SerializeField] private AudioClip _jump;
        [SerializeField] private AudioClip _step;

        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _locomotionController.OnJump += PlayJumpClip;
        }

        private void OnDestroy()
        {
            _locomotionController.OnJump -= PlayJumpClip;
        }

        private void PlayJumpClip()
        {
            _source.PlayOneShot(_jump);
        }

        //call from unity animator
        private void PerformOnStep()
        {
            _source.PlayOneShot(_jump);
        }
    }
}