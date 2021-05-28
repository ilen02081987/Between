using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 direction, float speed)
        {
            _rigidbody.velocity = direction * speed;
        }
    }
}