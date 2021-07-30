using UnityEngine;

namespace Between.InputTracking
{
    public struct InputData
    {
        public readonly int MouseButton;
        public readonly Vector2Int Position;

        public InputData(int mouseButton, Vector2Int position)
        {
            MouseButton = mouseButton;
            Position = position;
        }

        public bool IsDefault() => Position == Vector2Int.zero;
    }
}