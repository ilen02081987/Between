using Between.Collisions;
using Between.Utilities;
using System.Collections;
using UnityEngine;

namespace Between.MainCharacter
{
    public class LocomotionController : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _groundChecker;

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;

        [SerializeField] private float _gravity;
        [SerializeField] private float _pushStopSpeed;

        private float _velocityY;
        private bool _pressedJumpButton => Input.GetKeyDown(KeyCode.Space);
        //private bool _isGrounded => !SpaceDetector.IsFreeSpace(_groundChecker.position, .1f) || _characterController.isGrounded;
        private bool _isGrounded => 
            SpaceDetector.IsGrounded(_groundChecker.position) || _characterController.isGrounded;

        private bool _isPushed = false;

        private void Start()
        {
            _player.OnDie += () => enabled = false;
        }

        private void Update()
        {
            Move();
            TryJump();
        }

        public void Push(Vector3 direction, float force)
        {
            if (!_isPushed)
            {
                CoroutineLauncher.Start(PushTranslate(direction, force));
                _isPushed = true;
            }
        }

        private void Move()
        {
            Vector3 velocity = CalculateVelocity();
            _characterController.Move(velocity * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            if (_isGrounded && velocity.y < 0)
                _velocityY = 0f;
        }

        private void TryJump()
        {
            if (_pressedJumpButton && _isGrounded)
            {
                float jumpVelocity = Mathf.Sqrt(-2 * _gravity * _jumpHeight);
                _velocityY = jumpVelocity;
            }
        }

        private Vector3 CalculateVelocity()
        {
            float axisValue = Input.GetAxisRaw("Horizontal");

            _velocityY += Time.deltaTime * _gravity;
            Vector3 velocity = _speed * axisValue * Vector3.right + Vector3.up * _velocityY;
            return velocity;
        }

        private IEnumerator PushTranslate(Vector3 direction, float force)
        {
            while (enabled && force > 0f)
            {
                _characterController.Move(direction * force);
                force -= _pushStopSpeed;

                yield return null;
            }

            _isPushed = false;
        }
    }
}