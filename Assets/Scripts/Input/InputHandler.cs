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

        private InputData _previousInput;

        private Vector3 _mousePosition => Input.mousePosition;

        private int[] _mouseInputButtons = new int[] { 0, 1 };

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                StartDrawing(0);
        }

        private void StartDrawing(int mouseButton)
        {
            StartDraw?.Invoke(new InputData(mouseButton, _mousePosition));
        }
    }
}