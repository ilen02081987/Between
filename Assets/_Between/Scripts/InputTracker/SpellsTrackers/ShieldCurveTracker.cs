using System.Collections.Generic;
using UnityEngine;

namespace Between.InputTracking.Trackers
{
    public class ShieldCurveTracker : BaseInputTracker
    {
        public List<Vector2Int> DrawPoints { get; private set; } = new List<Vector2Int>();

        private float _minLenght => GameSettings.Instance.ShieldTrackerMinLenght;
        private float _maxLenght => GameSettings.Instance.ShieldTrackerMaxLenght;

        private bool _isValidLenght => InputLenghtCalculator.LastLenght > _minLenght && InputLenghtCalculator.LastLenght < _maxLenght;

        public ShieldCurveTracker(int mouseButton) : base(mouseButton) { }

        protected override void OnDrawStarted(InputData point) => DrawPoints.Add(point.Position);
        protected override void OnDrawCalled(InputData point) => DrawPoints.Add(point.Position);

        protected override void OnDrawEnded(InputData point)
        {
            if (_isValidLenght)
                InvokeCompleteEvent();
            else
                InvokeFailedEvent();

            Complete();
        }

        protected override void OnDrawForceEnded(InputData point) => Complete();

        private void Complete()
        {
            SetState(DrawState.None);
            Clear();
        }

        protected override void Clear()
        {
            DrawPoints.Clear();
        }
    }
}