using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput.Trackers
{
    public class CurveTracker : BaseInputTracker
    {
        public override int MouseButton => 1;

        public readonly float MaxLenght = 1000;
        public readonly float MinLenght = 500;

        public List<Vector3> DrawPoints { get; private set; } = new List<Vector3>();

        protected override void OnDrawStarted(InputData point)
        {
            if (CompareState(DrawState.Draw))
                ThrowException($"Can't start draw. It's already started.");

            SetState(DrawState.Draw);

            DrawPoints.Add(point.Position);
        }

        protected override void OnDrawCalled(InputData point)
        {
            if (CompareState(DrawState.Draw))
            {
                if (IsEnoughLong())
                    InvokeCanCompleteEvent();
                else if (!IsTooLong())
                    DrawPoints.Add(point.Position);
            }
        }

        protected override void OnDrawEnded(InputData point)
        {
            if (CompareState(DrawState.Draw))
            {
                SetState(DrawState.None);

                if (IsEnoughLong())
                {
                    InvokeCompleteEvent();
                    ClearTracker();
                }
            }
        }

        protected override void OnDrawForceEnded(InputData point)
        {
            if (CompareState(DrawState.Draw))
            {
                SetState(DrawState.None);
                ClearTracker();
            }
        }

        private bool IsEnoughLong() => CalculateLenght() > MinLenght;
        private bool IsTooLong() => CalculateLenght() > MaxLenght;

        public float CalculateLenght()
        {
            if (DrawPoints.Count > 2)
                return Vector3.Distance(DrawPoints[DrawPoints.Count - 1], DrawPoints[0]);
            else
                return 0f;
        }

        private void ClearTracker() => DrawPoints.Clear();
    }
}