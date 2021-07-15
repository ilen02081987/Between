using System;
using UnityEngine;

namespace Between.MainCharacter
{
    public class LocomotionController : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private float _detectRadius = .1f;

        [SerializeField] private float _acceleration = 2f;
        [SerializeField] private float _speed;
        [SerializeField, Range(0f, 1f)] private float _stopCoefficient = .5f;
        [SerializeField] private float _jumpForce;

        [SerializeField, Range(0f, 1f)] private float _airControl = .5f;

        private float _velocityY;

        private void Start()
        {
            _player.OnDie += () => enabled = false;
        }

        private void Update()
        {
            UpdateVerticalVelocity();
            Move();
            TryJump();
        }

        private void UpdateVerticalVelocity()
        {
            
        }

        private void Move()
        {
            float axisValue = Input.GetAxisRaw("Horizontal");
            Vector3 movementValue = _speed * axisValue * Vector3.right * Time.deltaTime;

            _characterController.Move(movementValue);
        }

        private void TryJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
            {
                //_characterController.velocity
            }
        }
    }
}