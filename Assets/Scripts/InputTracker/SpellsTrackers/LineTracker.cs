using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput.Trackers
{
    public class LineTracker : BaseInputTracker
    {
        public Vector3 LastDrawPoint => DrawPoints[DrawPoints.Count - 1];

        public List<Vector3> DrawPoints { get; private set; } = new List<Vector3>();

        public bool IsEnoughLong => CalculateLenght() > _minLenght;
        public bool IsTooLong => CalculateLenght() > _maxLenght;

        private readonly float _maxLenght = 1000;
        private readonly float _minLenght = 500;
        private readonly float _minTrackingLenght = 300f;


        public LineTracker(int mouseButton) : base(mouseButton) { }

        public override void Clear()
        {

        }

        protected override void OnDrawStarted(InputData input)
        {
            DrawPoints.Add(input.Position);
        }

        protected override void OnDrawCalled(InputData input)
        {
            
        }

        protected override void OnDrawEnded(InputData input)
        {
            
        }

        protected override void OnDrawForceEnded(InputData input)
        {
            
        }

        private float CalculateLenght()
        {
            if (DrawPoints.Count > 2)
                return Vector3.Distance(DrawPoints[DrawPoints.Count - 1], DrawPoints[0]);
            else
                return 0f;
        }
    }
}