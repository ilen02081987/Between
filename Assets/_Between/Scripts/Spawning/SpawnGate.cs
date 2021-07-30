using UnityEngine;
using Between.Enemies;
using Between.LevelObjects;

namespace Between.Spawning
{
    public class SpawnGate : MonoBehaviour
    {
        [SerializeField] private SpawnPoint[] _spawnPoints;
        [SerializeField] private FightBorders _fightBorders;

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
            {
                var spawnedObject = point.Spawn();
                TryAddEnemy(spawnedObject);
            }
        }

        private void Destroy()
        {
            foreach (SpawnPoint point in _spawnPoints)
                Destroy(point.gameObject);

            Destroy(gameObject);
        }

        private void TryAddEnemy(GameObject gameObject)
        {
            var enemy = gameObject.GetComponent<BaseEnemy>();

            if (_fightBorders != null && enemy != null)
                _fightBorders.AddEnemy(enemy);
        }
    }
}