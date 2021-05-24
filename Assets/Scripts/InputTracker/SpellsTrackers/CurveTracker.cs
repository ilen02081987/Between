using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput.Trackers
{
    public class CurveTracker : BaseInputTracker
    {
        public override int MouseButton => 1;

        private readonly float _lenght = 800;

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
                DrawPoints.Add(point.Position);

                if (IsEnoughLong())
                    InvokeCanCompleteEvent();
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

        private bool IsEnoughLong()
        {
            var distance = Vector3.Distance(DrawPoints[DrawPoints.Count - 1], DrawPoints[0]);
            //Debug.Log("Distance = " + distance);
            return distance > _lenght;
        }

        private void ClearTracker() => DrawPoints.Clear();
    }
}