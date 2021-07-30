using Between.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.InputTracking
{
    public static class InputLenghtCalculator
    {
        public static float LastLenght;
        private static Vector2Int _lastPoint;

        public static void Init()
        {
            InputHandler.StartDraw += ClearPreviousLenght;
            InputHandler.DrawCall += CollectInput;
        }

        public static void Dispose()
        {
            InputHandler.StartDraw -= ClearPreviousLenght;
            InputHandler.DrawCall -= CollectInput;
        }

        private static void ClearPreviousLenght(InputData obj)
        {
            LastLenght = 0f;
            _lastPoint = GameExtensions.DefaultPosition;
        }

        private static void CollectInput(InputData input)
        {
            if (_lastPoint == GameExtensions.DefaultPosition)
                _lastPoint = input.Position;

            LastLenght += Vector2.Distance(input.Position, _lastPoint);
            _lastPoint = input.Position;
        }
    }
}