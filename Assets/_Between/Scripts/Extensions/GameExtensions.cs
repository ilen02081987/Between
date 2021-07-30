using Between.InputTracking;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Extensions
{
    public static class GameExtensions
    {
        public static float DefaultAngle => float.MaxValue;
        public static Vector2Int DefaultPosition => new Vector2Int(-1, -1);

        public static bool IsDefaultAngle(this float value) => Mathf.Approximately(value, float.MaxValue);
        public static bool IsDefaultPosition(this Vector2Int value) => value == DefaultPosition;

        public static Vector2Int FindLeftPoint(this List<Vector2Int> points)
        {
            Vector2Int leftPoint = points[0];

            foreach (var point in points)
            {
                if (point.x < leftPoint.x)
                    leftPoint = point;
            }

            return leftPoint;
        }

        public static Vector2Int FindRightPoint(this List<Vector2Int> points)
        {
            Vector2Int rightPoint = points[0];

            foreach (var point in points)
            {
                if (point.x > rightPoint.x)
                    rightPoint = point;
            }

            return rightPoint;
        }

        public static Vector2Int FindUpperPoint(this List<Vector2Int> points)
        {
            Vector2Int upperPoint = points[0];

            foreach (var point in points)
            {
                if (point.y > upperPoint.y)
                    upperPoint = point;
            }

            return upperPoint;
        }

        public static Vector2Int ToVector2Int(this Vector3 original)
            => new Vector2Int((int)original.x, (int)original.y);

        public static Vector3 ToVector3(this Vector2Int original)
            => new Vector3(original.x, original.y, 0f);

        public static float Angle(this Vector2 vector)
            => Vector2.Angle(Vector2.right, vector);

        public static float Angle(this Vector2Int vector)
            => Vector2.Angle(Vector2.right, vector);

        public static float Angle(this InputData input)
            => input.Position.Angle();

        public static List<Vector3> ToWorldPoints(this List<Vector2Int> original)
        {
            List<Vector3> output = new List<Vector3>();

            foreach (var point in original)
                output.Add(GameCamera.ScreenToWorldPoint(point));

            return output;
        }
    }
}