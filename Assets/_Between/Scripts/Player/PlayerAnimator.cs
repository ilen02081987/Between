using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.MainCharacter
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private LocomotionController _locomotionController;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            _locomotionController.OnRun += Move;
            _locomotionController.OnStop += Stop;
            _locomotionController.OnJump += Jump;
        }

        private void OnDestroy()
        {
            _locomotionController.OnRun -= Move;
            _locomotionController.OnStop -= Stop;
            _locomotionController.OnJump -= Jump;
        }

        private void Move(float relativeSpeed)
        {
            _animator.SetFloat("Move", relativeSpeed);
        }

        private void Stop()
        {
            _animator.SetFloat("Move", 0f);
            _animator.SetTrigger("Idle");
        }

        private void Jump()
        {
            _animator.SetFloat("Move", 0f);
            _animator.SetTrigger("Jump");
        }
    }
}