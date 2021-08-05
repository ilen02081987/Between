using Between.MainCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Between.Sounds
{
    public class PlayerSoundsController : BaseSoundsController
    {
        [SerializeField] private LocomotionController _locomotionController;

        [Space]
        [SerializeField] private AudioClip _jump;
        [SerializeField] private AudioClip _step;

        private void Start()
        {
            _locomotionController.OnJump += PlayJumpClip;
        }

        private void OnDestroy()
        {
            _locomotionController.OnJump -= PlayJumpClip;
        }

        private void PlayJumpClip() => Play(_jump);

        //call from unity animator
        private void PerformOnStep() => Play(_step);
    }
}