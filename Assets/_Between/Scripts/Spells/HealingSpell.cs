using System;
using System.Collections.Generic;
using UnityEngine;
using Between.Extensions;
using Between.InputTracking.Trackers;
using Between.SpellRecognition;

namespace Between.Spells
{
    public class HealingSpell : SvmBasedSpell
    {
        protected override SpellFigure _figure => SpellFigure.Circle;
        protected override float _manaCoefficient => GameSettings.Instance.HealingSpellManaCoefficient;

        private float _maxSize => GameSettings.Instance.HealingSpellMaxSize;
        private float _maxHeal => GameSettings.Instance.HealingSpellMaxHeal;

        private bool _enoughMana => EnoughMana(_clampedSpellLenght);
        private float _clampedSpellLenght => Mathf.Min(_spellLenght, _maxSize);

        protected override void OnCompleteSpell()
        {
            if (!ValidCompressionRatio())
            {
                InvokeNotRecognizeEvent();
                return;
            }

            if (!_enoughMana)
            {
                InvokeNotEnoughManaEvent();
                return;
            }

            TryHealPlayer();
            SpendManaForSpell(_clampedSpellLenght);
        }

        private void TryHealPlayer()
        {
            float healValue = _spellLenght / _maxSize * _maxHeal;

            if (ContainsPlayer())
                Player.Instance.Controller.Heal(healValue);
        }

        private bool ValidCompressionRatio()
        {
            List<Vector2Int> points = ((SvmTracker)tracker).DrawPoints;
            
            float minDistance = float.MaxValue;
            float maxDistance = 0f;

            for (int i = 0; i < points.Count; i++)
            {
                int oppositePointNumber = (i + points.Count / 2) % points.Count;
                float currentDistance = Vector2Int.Distance(points[i], points[oppositePointNumber]);

                if (currentDistance < minDistance)
                    minDistance = currentDistance;

                if (currentDistance > maxDistance)
                    maxDistance = currentDistance;
            }

            return maxDistance / minDistance < GameSettings.Instance.CircleCompressionRatio;
        }

        private bool ContainsPlayer()
        {
            List<Vector2Int> points = ((SvmTracker)tracker).DrawPoints;
            Vector3 worldMiddlePoint = FindWorldMiddlePoint(points);
            var minWorldDiameter = FindAverageWorldDiameter(points);

            Collider[] colliders = Physics.OverlapSphere(worldMiddlePoint, minWorldDiameter / 2f);

            if (colliders == null || colliders.Length == 0)
                return false;

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<PlayerController>(out var player))
                    return true;
            }

            return false;
        }

        private float FindAverageWorldDiameter(List<Vector2Int> points)
        {
            List<Vector3> worldPoints = points.ToWorldPoints();
            int pointsHalfCount = points.Count / 2;
            float totalDistance = default;
            int oppositePointNumber = default;

            for (int i = 0; i < worldPoints.Count; i++)
            {
                oppositePointNumber = (i + pointsHalfCount) % worldPoints.Count;
                totalDistance += Vector3.Distance(worldPoints[i], worldPoints[oppositePointNumber]);
            }

            return totalDistance / points.Count;
        }

        private float FindWorldRadius(float coefficient, List<Vector2Int> points)
        {
            var radius = FindAverageRadius(points);
            return radius * coefficient;
        }

        private float FindAverageRadius(List<Vector2Int> circlePoints)
        {
            DateTime startCountTime = DateTime.Now;
            float totalDistance = default;

            for (int i = 0; i < circlePoints.Count; i++)
                for (int j = 0; j < circlePoints.Count; j++)
                    totalDistance += Vector2Int.Distance(circlePoints[i], circlePoints[j]);

            float radius = totalDistance / (circlePoints.Count * circlePoints.Count);
            
            //Debug.Log($"circle points count = {circlePoints.Count}, radius = {radius}, " +
            //    $"calculate time ms = {DateTime.Now.Subtract(startCountTime).TotalMilliseconds}");

            return radius;
        }

        private Vector2Int FindFarthestPoint(List<Vector2Int> points, Vector2Int firstPoint)
        {
            Vector2Int farthestPoint = points[0];
            float farthestDistance = 0f;

            foreach (Vector2Int point in points)
            {
                var currentDistance = Vector2Int.Distance(firstPoint, point);

                if (currentDistance > farthestDistance)
                {
                    farthestPoint = point;
                    farthestDistance = currentDistance;
                }
            }

            return farthestPoint;
        }

        private Vector3 FindWorldMiddlePoint(List<Vector2Int> points)
        {
            Vector2Int firstPoint = points[0];
            Vector2Int farthestPoint = FindFarthestPoint(points, firstPoint);

            Vector2Int middlePoint = Vector2Int.RoundToInt(Vector2.Lerp(firstPoint, farthestPoint, .5f));
            return GameCamera.ScreenToWorldPoint(middlePoint);
        }

        private float FindWorldPointsDistance(Vector2Int first, Vector2Int second)
        {
            return Vector3.Distance(
                GameCamera.ScreenToWorldPoint(first), 
                GameCamera.ScreenToWorldPoint(second));
        }
    }
}