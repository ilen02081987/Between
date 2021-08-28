using System;
using UnityEngine;

namespace Between.InputTracking
{
    public class MouseInput : IComparable<MouseInput>
    {
        public readonly int MouseButton;
        public Vector3 InputPosition => Input.mousePosition;
        public int Priority => _priority;
        public bool IsActive => GetState() != InputState.None && GetState() != InputState.ForceEnd;

        public InputState State { get => GetState(); set => State = value; }

        private int _priority;

        public MouseInput(int mouseButtonNumber, int prioryty)
        {
            MouseButton = mouseButtonNumber;
            _priority = prioryty;
        }

        public int CompareTo(MouseInput other) => Priority.CompareTo(other.Priority);

        private InputState GetState()
        {
            if (Input.GetMouseButtonDown(MouseButton) && !PauseManager.IsPause)
                return InputState.Start;

            if (Input.GetMouseButton(MouseButton) && !PauseManager.IsPause)
                return InputState.Draw;

            if (Input.GetMouseButtonUp(MouseButton) && !PauseManager.IsPause)
                return InputState.End;

            return InputState.None;
        }

    }

    public enum InputState
    {
        None = 0,
        Start,
        Draw,
        End,
        ForceEnd
    }
}