using Between.InputTracking.Trackers;
using Between.SpellRecognition;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells
{
    public class HealingSpell : SvmBasedSpell
    {
        public override float CoolDownTime => GameSettings.Instance.HealingSpellCooldown;

        protected override SpellFigure _figure => SpellFigure.Circle;

        protected override float _manaCoefficient => GameSettings.Instance.HealingSpellManaCoefficient;

        protected override void OnCompleteSpell()
        {
            if (_enoughMana)
            {
                TryHealPlayer();
                RemoveMana();
            }
        }

        private void TryHealPlayer()
        {
            GameSettings settings = GameSettings.Instance;
            float healValue = _spellLenght / settings.HealingSpellMaxSize * settings.HealingSpellMaxHeal;

            if (ContainsPlayer())
                Player.Instance.Controller.Heal(healValue);
        }

        private bool ContainsPlayer()
        {
            List<Vector2Int> points = ((SvmTracker)tracker).DrawPoints;
            Vector2Int firstPoint = points[0];
            Vector2Int farthestPoint = FindFarthestPoint(points, firstPoint);

            Vector2Int middlePoint = Vector2Int.RoundToInt(Vector2.Lerp(firstPoint, farthestPoint, .5f));
            Vector3 worldMiddlePoint = GameCamera.ScreenToWorldPoint(middlePoint);
            float worldCircleSize = FindWorldPointsDistance(firstPoint, farthestPoint);

            Collider[] colliders = Physics.OverlapSphere(worldMiddlePoint, worldCircleSize / 2f);

            if (colliders == null || colliders.Length == 0)
                return false;

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<PlayerController>(out var player))
                    return true;
            }

            return false;
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

        private float FindAveragePointsDistance(List<Vector2Int> points)
        {
            float distancies = default;
            Vector2Int firstPoint = points[0];

            foreach (var point in points)
                distancies += Vector2Int.Distance(firstPoint, point);

            return distancies / points.Count;
        }

        private float FindWorldPointsDistance(Vector2Int first, Vector2Int second)
        {
            return Vector3.Distance(
                GameCamera.ScreenToWorldPoint(first), 
                GameCamera.ScreenToWorldPoint(second));
        }
    }
}