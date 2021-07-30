using UnityEngine;
using Between.Spawning;
using Cinemachine;

namespace Between
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private SpawnPoint _startPlayerSpawnPoint;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public void Init()
        {
            if (_startPlayerSpawnPoint != null)
            {
                GameObject player = _startPlayerSpawnPoint.Spawn();
                _virtualCamera.Follow = player.transform;
            }
        }
    }
}