using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Between.SpellsEffects.ShieldSpell
{
    public class ShieldSpawner
    {
        private Shield _shield;
        private GameObject _shieldsParent;
        private readonly Transform _owner;

        public ShieldSpawner(string prefabName, Transform owner)
        {
            _shield = Resources.Load<Shield>(Path.Combine(ResourcesFoldersNames.SPELLS, prefabName));
            _shieldsParent = new GameObject("ShieldsParent");
            _owner = owner;
        }

        public void Spawn(Vector3 from, Vector3 to)
        {
            var shieldsSpawnPoints = CalculateShieldPositions(new List<Vector3> { from, to });
            SpawnShields(shieldsSpawnPoints);
        }

        public void SpawnFromScreenPoints(List<Vector2Int> curve)
        {
            var shieldsSpawnPoints = CalculateShieldPositions(ConvertToGameSpace(curve));
            SpawnShields(shieldsSpawnPoints);
        }

        private void SpawnShields(List<Vector3> shieldsSpawnPoints)
        {
            foreach (var point in shieldsSpawnPoints)
                SpawnSingleShield(point);
        }

        private List<Vector3> CalculateShieldPositions(List<Vector3> points)
        {
            var shieldSize = _shield.Size;
            var shieldPoints = new List<Vector3>() { points[0] };

            for (int i = 0; i < points.Count; i++)
            {
                var lastShieldPoint = shieldPoints[shieldPoints.Count - 1];
                var distance = Vector3.Distance(points[i], lastShieldPoint);

                if (distance > shieldSize)
                {
                    var shieldPoint = Vector3.Lerp(lastShieldPoint, points[i], shieldSize / distance);
                    shieldPoints.Add(shieldPoint);
                    i--;
                }
            }

            return shieldPoints;
        }

        private List<Vector3> ConvertToGameSpace(List<Vector2Int> points)
        {
            var outPoint = new List<Vector3>();

            foreach (var point in points)
                outPoint.Add(GameCamera.ScreenToWorldPoint(point));

            return outPoint;
        }

        private void SpawnSingleShield(Vector3 point)
        {
            var shield = MonoBehaviour.Instantiate(_shield, point, Quaternion.identity, _shieldsParent.transform);
            shield.transform.LookAt(_owner);
        }
    }
}