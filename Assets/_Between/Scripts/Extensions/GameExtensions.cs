using System.Collections.Generic;
using UnityEngine;

namespace Between.Extensions
{
    public static class GameExtensions
    {
        public static float DefaultAngle => float.MaxValue;
        public static Vector3 DefaultPosition => Vector3.forward;

        public static bool IsDefaultAngle(this float value) => Mathf.Approximately(value, float.MaxValue);
        public static bool IsDefaultPosition(this Vector3 value) => value == Vector3.forward;

        public static Vector3 FindLeftPoint(this List<Vector3> points)
        {
            Vector3 leftPoint = points[0];

            foreach (var point in points)
            {
                if (point.x < leftPoint.x)
                    leftPoint = point;
            }

            return leftPoint;
        }

        public static Vector3 FindRightPoint(this List<Vector3> points)
        {
            Vector3 rightPoint = points[0];

            foreach (var point in points)
            {
                if (point.x > rightPoint.x)
                    rightPoint = point;
            }

            return rightPoint;
        }

        public static Vector3 FindUpperPoint(this List<Vector3> points)
        {
            Vector3 upperPoint = points[0];

            foreach (var point in points)
            {
                if (point.y > upperPoint.y)
                    upperPoint = point;
            }

            return upperPoint;
        }
    }
}