using Between.Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Sounds
{
    public class SpawnPointSoundsController : BaseSoundsController
    {
        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private AudioClip _clip;

        private void Start()
        {
            _spawnPoint.OnSpawn += PlaySound;
        }

        private void PlaySound()
        {
            _spawnPoint.OnSpawn -= PlaySound;
            Play(_clip);
        }
    }
}