using System;
using UnityEngine;

namespace Between.Spawning
{
    public class SpawnPoint : MonoBehaviour
    {
        public event Action OnSpawn;

        [SerializeField] private GameObject _spawnedObject;
        [SerializeField] private Transform _parent;

        public GameObject Spawn()
        {
            OnSpawn?.Invoke();
            return Instantiate(_spawnedObject, transform.position, Quaternion.identity, _parent);
        }
    }
}