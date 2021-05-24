using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput
{
    public class SingleButtonMouseInput : IInput
    {
        public readonly int MouseButton;
        public Vector3 InputPosition => Input.mousePosition;
        public int Priority => _priority;
        public bool IsActive => GetState() != InputState.None;

        private int _priority;

        public SingleButtonMouseInput(int mouseButtonNumber, int prioryty)
        {
            MouseButton = mouseButtonNumber;
            _priority = prioryty;
        }

        public InputState GetState()
        {
            if (Input.GetMouseButtonDown(MouseButton))
                return InputState.Start;
            if (Input.GetMouseButton(MouseButton))
                return InputState.Draw;
            if (Input.GetMouseButtonUp(MouseButton))
                return InputState.End;

            return InputState.None;
        }
    }
}