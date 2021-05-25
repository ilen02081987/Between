using UnityEngine;

namespace Between.UserInput
{
    public struct InputData
    {
        public readonly int MouseButton;
        public readonly Vector3 Position;
        public readonly float Angle;

        public InputData(int mouseButton, Vector3 position, Vector3 previousPosition) : this(mouseButton, position)
        {
            var diff = position - previousPosition;
            Angle = Vector3.Angle(Vector3.right, diff);
        }

        public InputData(int mouseButton, Vector3 position, float angle = 0f)
        {
            MouseButton = mouseButton;
            Position = position;
            Angle = angle;
        }
    }
}