using UnityEngine;
using Between.Enemies;
using Between.Spawning;

namespace Between.LevelObjects
{
    public class LoadPoint : MonoBehaviour
    {
        [SerializeField] private SpawnPoint _playerSpawnPoint;
        [SerializeField] private BaseEnemy[] _enemies;
        [SerializeField] private SpawnGate[] _spawnGates;

        public void Clear()
        {
            foreach (var enemy in _enemies)
                Destroy(enemy.gameObject);

            foreach (var spawnGate in _spawnGates)
                spawnGate.Destroy();
        }

        public Player LoadPlayer()
        {
            var player = _playerSpawnPoint.Spawn();
            return new Player(player.GetComponent<PlayerController>());
        }
    }
}