using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput
{
    public interface IInput
    {
        bool IsActive { get; }
        Vector3 InputPosition { get; }
        int Priority { get; }
        InputState GetState();
    }

    public enum InputState
    {
        None = 0,
        Start,
        Draw,
        End
    }
}