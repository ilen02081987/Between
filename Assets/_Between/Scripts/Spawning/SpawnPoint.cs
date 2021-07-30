using TMPro;
using UnityEngine;

namespace Between.Spawning
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnedObject;
        [SerializeField] private Transform _parent;

        public GameObject Spawn()
        {
            return Instantiate(_spawnedObject, transform.position, Quaternion.identity, _parent);
        }
    }
}