using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Between.SpellsEffects.Shield
{
    public class ShieldSpawner
    {
        private Shield _shield;
        private GameObject _shieldsParent;

        public ShieldSpawner()
        {
            _shield = Resources.Load<Shield>(Path.Combine(ResourcesFoldersNames.SPELLS, "Shield"));
            _shieldsParent = new GameObject("ShieldsParent");
        }

        public void Spawn(List<Vector3> curve)
        {
            var shieldsSpawnPoints = CalculateShieldPositions(curve);

            foreach (var item in shieldsSpawnPoints)
            {

            }
        }

        private List<Vector3> CalculateShieldPositions(List<Vector3> points)
        {
            var shieldSize = _shield.Size;
            var shieldPoints = new List<Vector3>() { points[0] };

            foreach (var point in points)
            {
                var lastShieldPointIndex = shieldPoints.Count - 1;
                var distance = Vector3.Distance(point, shieldPoints[lastShieldPointIndex]);

                if (distance > shieldSize)
                    shieldPoints.Add(point);
            }

            return shieldPoints;
        }

        private void SpawnShield(Vector3 point)
        {
            //MonoBehaviour.Instantiate(_shield, )
        }
    }
}