using UnityEngine;

namespace Between.Extensions
{
    public static class GameExtensions
    {
        public static float DefaultAngle => float.MaxValue;
        public static Vector3 DefaultPosition => Vector3.forward;

        public static bool IsDefaultAngle(this float value) => Mathf.Approximately(value, float.MaxValue);
        public static bool IsDefaultPosition(this Vector3 value) => value == Vector3.forward;
    }
}