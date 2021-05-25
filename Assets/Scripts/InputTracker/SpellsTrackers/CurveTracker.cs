using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput.Trackers
{
    public class CurveTracker : BaseInputTracker
    {
        public List<Vector3> DrawPoints { get; private set; } = new List<Vector3>();

        protected override int MouseButton => 1;

        private readonly float _maxLenght = 1000;
        private readonly float _minLenght = 500;
        private readonly float _forceEndAngle = 90f;

        private float _startAngle;

        protected override void OnDrawStarted(InputData point)
        {
            if (CompareState(DrawState.Draw))
                ThrowException($"Can't start draw. It's already started.");

            SetState(DrawState.Draw);

            DrawPoints.Add(point.Position);
        }

        protected override void OnDrawCalled(InputData point)
        {
            TrySetStartAngle(point);

            if (IsTooCurve(point))
            {
                InvokeFailedEvent();
                SetState(DrawState.None);
                ClearTracker();

                return;
            }

            if (IsEnoughLong())
                InvokeCanCompleteEvent();
            else if (!IsTooLong())
                DrawPoints.Add(point.Position);
        }

        protected override void OnDrawEnded(InputData point)
        {
            SetState(DrawState.None);

            if (IsEnoughLong())
                InvokeCompleteEvent();

            ClearTracker();
        }

        protected override void OnDrawForceEnded(InputData point)
        {
            SetState(DrawState.None);
            ClearTracker();
        }

        private bool IsTooCurve(InputData point) => Mathf.Abs(point.Angle - _startAngle) > _forceEndAngle;
        private bool IsEnoughLong() => CalculateLenght() > _minLenght;
        private bool IsTooLong() => CalculateLenght() > _maxLenght;

        private float CalculateLenght()
        {
            if (DrawPoints.Count > 2)
                return Vector3.Distance(DrawPoints[DrawPoints.Count - 1], DrawPoints[0]);
            else
                return 0f;
        }

        private void TrySetStartAngle(InputData point)
        {
            if (Mathf.Approximately(_startAngle, 0f))
                _startAngle = point.Angle;
        }

        private void ClearTracker()
        {
            DrawPoints.Clear();
            _startAngle = default;
        }
    }
}