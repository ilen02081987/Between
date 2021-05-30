using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private Vector3 _velocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 velocity)
        {
            _velocity = velocity;
        }

        private void Update()
        {
            _rigidbody.velocity = _velocity;
        }
    }
}