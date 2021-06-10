using System.Collections.Generic;
using UnityEngine;
using Between.Extensions;

namespace Between.UserInput.Trackers
{
    public class CurveTracker : BaseInputTracker
    {
        public Vector3 LastDrawPoint => DrawPoints[DrawPoints.Count - 1];
        public List<Vector3> DrawPoints { get; private set; } = new List<Vector3>();

        public bool IsEnoughLong => CalculateLenght() > _minLenght;
        public bool IsTooLong => CalculateLenght() > _maxLenght;

        private float _maxLenght = 1000;
        private float _minLenght = 500;
        private readonly float _minTrackingLenght = 50f;

        private float _forceEndAngle = 30f;

        private Vector3 _startPosition = GameExtensions.DefaultPosition;
        private float _startAngle = GameExtensions.DefaultAngle;

        private bool _isEnoughLongToTrack => CalculateLenght() > _minTrackingLenght;

        public CurveTracker(int mouseButton) : base(mouseButton) { }

        public CurveTracker SetForceEndAngle(float angle)
        {
            _forceEndAngle = angle;
            return this;
        }

        public CurveTracker SetLenght(float min, float max)
        {
            _minLenght = min;
            _maxLenght = max;

            return this;
        }

        public override void Clear()
        {
            DrawPoints.Clear();

            _startPosition = GameExtensions.DefaultPosition;
            _startAngle = GameExtensions.DefaultAngle;
        }

        protected override void OnDrawStarted(InputData point) => DrawPoints.Add(point.Position);

        protected override void OnDrawCalled(InputData point)
        {
            TrySetStartPosition(point);
            TrySetStartAngle(point);

            if (IsTooCurve(point))
            {
                Debug.Log("Is too curve");

                InvokeFailedEvent();
                Complete();

                return;
            }

            if (IsEnoughLong)
                InvokeCanCompleteEvent();
            
            if (!IsTooLong)
                DrawPoints.Add(point.Position);
        }

        protected override void OnDrawEnded(InputData point)
        {
            if (IsEnoughLong)
                InvokeCompleteEvent();
            else
                InvokeFailedEvent();

            Complete();
        }

        protected override void OnDrawForceEnded(InputData point) => Complete();

        #region PRIVATE METHODS

        public bool IsTooCurve(InputData point) => 
            _isEnoughLongToTrack ? Mathf.Abs(point.Angle - _startAngle) > _forceEndAngle : false;

        private float CalculateLenght()
        {
            if (DrawPoints.Count > 2)
                return Vector3.Distance(DrawPoints[DrawPoints.Count - 1], DrawPoints[0]);
            else
                return 0f;
        }

        private void TrySetStartPosition(InputData point)
        {
            if (_startPosition.IsDefaultPosition())
                _startPosition = point.Position;
        }

        private void TrySetStartAngle(InputData point)
        {
            if (_isEnoughLongToTrack && _startAngle.IsDefaultAngle())
                _startAngle = Vector3.Angle(Vector3.right, (point.Position - _startPosition).normalized);
        }

        private void Complete()
        {
            SetState(DrawState.None);
            Clear();
        }

        #endregion
    }
}