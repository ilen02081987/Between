using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput
{
    public class InputHandler : MonoBehaviour
    {
        public static event Action<InputData> StartDraw;
        public static event Action<InputData> DrawCall;
        public static event Action<InputData> EndDraw;

        private List<IInput> _mouseInputButtons = new List<IInput> { new SingleButtonMouseInput(0, 0), new SingleButtonMouseInput(1, 1) };
        private InputData _previousInput;
        private Vector3 _mousePosition => Input.mousePosition;

        private void Update()
        {
            //foreach (var item in _mouseInputButtons.()
            //{

            //}
        }
    }
}