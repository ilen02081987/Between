using UnityEngine;

namespace Between.UserInput
{
    public struct InputData
    {
        public readonly int MouseButton;
        public readonly Vector3 Position;
        public readonly float Derivative;

        public InputData(int mouseButton, Vector3 position, Vector3 previousPosition) : this(mouseButton, position)
        {
            var diff = position - previousPosition;
            var derivative = diff.y / diff.x;

            Derivative = derivative;
        }

        public InputData(int mouseButton, Vector3 position, float derivative = 0f)
        {
            MouseButton = mouseButton;
            Position = position;
            Derivative = derivative;
        }
    }
}