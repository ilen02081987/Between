using UnityEngine;
using Between.Teams;

namespace Between
{ 
    public class PlayerController : BaseDamagableObject
    {
        [SerializeField] private Rigidbody _body;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private float _detectRadius = .1f;

        [Header("Move settings")]
        [SerializeField] private float _acceleration = 2f;
        [SerializeField] private float _maxSpeed;
        [SerializeField, Range(0f, 1f)]
        private float _stopCoefficient = .5f;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpForceInAir;

        [SerializeField] private float _mass = 1f;
        [SerializeField] private float _gravity = 9.81f;
        [SerializeField, Range(0f, 1f)] private float _airControl = .5f;

        public override Team Team => Team.Player;

        private void Update()
        {
            TryMove(Input.GetAxisRaw("Horizontal"));

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }

        private void TryMove(float axis)
        {
            if (Mathf.Approximately(axis, 0f) && IsGrounded())
            {
                _body.velocity = new Vector3(
                    Mathf.Lerp(_body.velocity.x, 0f, _stopCoefficient),
                    _body.velocity.y,
                    _body.velocity.z);
            }
            else
            {
                float currentSpeed = axis * _acceleration;

                if (IsGrounded())
                    _body.velocity += Vector3.right * currentSpeed;
                else
                    _body.velocity += Vector3.right * currentSpeed * _airControl;

                _body.velocity = new Vector3(
                    Mathf.Clamp(_body.velocity.x, -_maxSpeed, _maxSpeed),
                    _body.velocity.y,
                    _body.velocity.z);
            }
        }

        private void Jump()
        {
            if (IsGrounded())
                _body.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            else
                _body.AddForce(Vector3.up * _jumpForceInAir, ForceMode.Impulse);
        }

        private bool IsGrounded()
        {
            var colliders = Physics.OverlapSphere(_groundChecker.position, _detectRadius);
            return colliders.Length > 1;
        }

        private void OnValidate()
        {
            if (!Mathf.Approximately(Physics.gravity.y, _gravity))
                Physics.gravity = new Vector3(0f, -_gravity, 0f);

            if (!Mathf.Approximately(_mass, _body.mass))
                _body.mass = _mass;
        }

        protected override void PerformOnDie()
        {
            Destroy(gameObject);
        }
    }
}