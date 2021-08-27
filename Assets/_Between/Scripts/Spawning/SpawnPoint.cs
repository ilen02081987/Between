using System;
using UnityEngine;

namespace Between.Spawning
{
    public class SpawnPoint : MonoBehaviour
    {
        public event Action OnSpawn;

        [SerializeField] private GameObject _spawnedObject;
        [SerializeField] private Transform _atPoint;
        [SerializeField] private Transform _parent;

        private Vector3 _position => _atPoint != null ? _atPoint.position : transform.position;

        public GameObject Spawn()
        {
            OnSpawn?.Invoke();
            return Instantiate(_spawnedObject, _position, Quaternion.identity, _parent);
        }
    }
}