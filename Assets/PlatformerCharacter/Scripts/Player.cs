using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Between.Teams;
using Between.Interfaces;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private Rigidbody _body;
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _detectRadius = .1f;
    [SerializeField] private float _health = 10;

    [Header("Можно менять")]

    [Tooltip("Скорость движения"), SerializeField] private float _maxSpeed;
    [Tooltip("Чем выше, тем быстрее останавливается, когда отжаты клавиши движения"), SerializeField, Range(0f, 1f)] 
    private float _stopCoefficient = .5f;
    [Tooltip("Сила прыжка"), SerializeField] private float _jumpForce;
    [Tooltip("Сила двойного прыжка"), SerializeField] private float _jumpForceInAir;

    [Tooltip("Масса персонажа"), SerializeField] private float _mass = 1f;
    [Tooltip("Гравитация"), SerializeField] private float _gravity = 9.81f;
    [Tooltip("Контроль скорости в прыжке, 1 - полный контроль, 0 - никакого изменения скорости"), SerializeField, Range(0f, 1f)]
    private float _airControl = .5f;

    public Team Team { get; set; } = Team.Enemies;
    public void ApplyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        TryMove(Input.GetAxis("Horizontal"));
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
            if (IsGrounded())
                _body.velocity += Vector3.right * axis;
            else
                _body.velocity += Vector3.right * axis * _airControl;

            _body.velocity = new Vector3(
                Mathf.Clamp(_body.velocity.x, -_maxSpeed, _maxSpeed),
                _body.velocity.y,
                _body.velocity.z);
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _body.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        } else
        {
            _body.AddForce(Vector3.up * _jumpForceInAir, ForceMode.Impulse);
        }
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





}
