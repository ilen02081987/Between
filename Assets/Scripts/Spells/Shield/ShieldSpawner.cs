using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells.Shield
{
    public class ShieldSpawner
    {
        private Shield _prefab;

        public ShieldSpawner()
        {
            _prefab = Resources.Load<Shield>("Shield");
        }

        public void SpawnShields(Transform owner, params Vector3[] screenPoints)
        {
            foreach (var point in screenPoints)
            {
                var worldPoint = GameCamera.ScreenToWorldPoint(point);
                SpawnShield(owner, worldPoint);
            }
        }

        private void SpawnShield(Transform owner, Vector3 position)
        {
            var shield = MonoBehaviour.Instantiate(_prefab, position, Quaternion.identity);
            shield.transform.LookAt(owner);
        }
    }
}