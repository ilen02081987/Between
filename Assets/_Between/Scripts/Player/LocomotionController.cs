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

        private float _velocityY;

        private bool _pressedJumpButton => Input.GetKeyDown(KeyCode.Space);
        private bool _isGrounded
        {
            get
            {
                var colliders = Physics.OverlapSphere(_groundChecker.position, .1f);
                var physicsCollide = colliders != null && colliders.Length > 0;

                return physicsCollide || _characterController.isGrounded;
            }
        }

        private void Start()
        {
            _player.OnDie += () => enabled = false;
        }

        private void Update()
        {
            Move();
            TryJump();
        }

        private void Move()
        {
            Vector3 velocity = CalculateVelocity();
            _characterController.Move(velocity * Time.deltaTime);

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
    }
}