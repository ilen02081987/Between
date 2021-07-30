using UnityEngine;

namespace Between.Spawning
{
    public class SpawnGate : MonoBehaviour
    {
        [SerializeField] private SpawnPoint[] _spawnPoints;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                Spawn();
                Destroy();
            }
        }

        private void Spawn()
        {
            foreach (SpawnPoint point in _spawnPoints)
                point.Spawn();
        }

        private void Destroy()
        {
            foreach (SpawnPoint point in _spawnPoints)
                Destroy(point.gameObject);

            Destroy(gameObject);
        }
    }
}