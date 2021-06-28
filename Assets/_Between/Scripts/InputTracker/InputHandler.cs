using System;
using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput
{
    public class InputHandler : MonoBehaviour
    {
        public static event Action<InputData> StartDraw;
        public static event Action<InputData> DrawCall;
        public static event Action<InputData> EndDraw;
        public static event Action<InputData> ForceEndDraw;

        public static Vector2Int MousePosition
        {
            get
            {
                Vector3 mousePosition = Input.mousePosition;
                return new Vector2Int((int)mousePosition.x, (int)mousePosition.y);
            }
        }

        private List<MouseInput> _inputs = new List<MouseInput> 
            { new MouseInput(0, 0), new MouseInput(1, 1) };

        private InputData _previousInput;

        private void Start()
        {
            _inputs.Sort();
        }

        private void Update()
        {
            bool hasActive = false;

            foreach (var input in _inputs)
            {
                if (!input.IsActive)
                    continue;

                if (!hasActive)
                {
                    SendActiveEvent(input);
                    hasActive = true;
                }
                else
                {
                    TrySendForceEndEvent(input);
                }
            }
        }

        private void SendActiveEvent(MouseInput input)
        {
            InputData newInput = CreateInputData(input);

            switch (input.State)
            {
                case InputState.Start:
                    StartDraw?.Invoke(newInput);
                    _previousInput = newInput;
                    break;
                case InputState.Draw:
                    if (newInput.Position != _previousInput.Position)
                    {
                        DrawCall?.Invoke(newInput);
                        _previousInput = newInput;
                    }
                    break;
                case InputState.End:
                    EndDraw?.Invoke(newInput);
                    _previousInput = default;
                    break;
                default:
                    throw new Exception("Try send active event to disactive input");
            }
        }

        private void TrySendForceEndEvent(MouseInput input) => ForceEndDraw?.Invoke(CreateInputData(input));

        private InputData CreateInputData(MouseInput input) 
            => new InputData(input.MouseButton, MousePosition);
    }
}