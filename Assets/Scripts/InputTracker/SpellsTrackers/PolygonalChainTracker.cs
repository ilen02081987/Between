using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput.Trackers
{
    public class PolygonalChainTracker : BaseInputTracker
    {
        public List<Vector3> Verticies { get; private set; } = new List<Vector3>();

        private List<CurveTracker> _trackers = new List<CurveTracker>();
        private List<float> _angles = new List<float>();
        private bool _isClosure = false;

        private float _closureDistance = 100f;

        public PolygonalChainTracker(int mouseButton) : base(mouseButton) { }
        
        #region FLUENT BUILDER

        public PolygonalChainTracker AddLineAtAngle(float angle)
        {
            _angles.Add(angle);
            return AddLine();
        }

        public PolygonalChainTracker AddLine()
        {
            _trackers.Add(new CurveTracker(_mouseButton));
            return this;
        }

        public PolygonalChainTracker AddClosure()
        {
            _isClosure = true;
            return this;
        }

        #endregion

        protected override void OnDrawStarted(InputData point)
        {
            Debug.Log("Init first curve");

            Verticies.Add(point.Position);
            InitLine(0);
        }

        private void AddVertex()
        {
            var index = GetLastTrackerIndex();
            Verticies.Add(_trackers[index].LastDrawPoint);
        }

        private void CheckFailedLine()
        {
            Debug.Log("Curve is failed");

            AddVertex();

            var index = GetLastTrackerIndex();

            if (_trackers.Count > index + 1 && _trackers[index].IsEnoughLong)
            {
                Debug.Log("Init next curve");
                InitLine(index + 1);
                DisposeLine(index);
            }
            else
                CheckFigure();
        }

        private void CheckCompleteLine()
        {
            AddVertex();
            CheckFigure();
        }

        private void CheckFigure()
        {
            Debug.Log("CheckFigure");

            if (IsFigureComplete())
                InvokeCompleteEvent();
            else
                InvokeFailedEvent();

            Clear();
        }

        private bool IsFigureComplete()
        {
            if (Verticies.Count != _trackers.Count + 1)
                return false;

            for (int i = 0; i < _angles.Count; i++)
            {
                var previousLine = (Verticies[i + 1] - Verticies[i]).normalized;
                var nextLine = (Verticies[i + 1] - Verticies[i + 2]).normalized;

                if (Vector3.Angle(previousLine, nextLine) < _angles[i])
                    return false;
            }

            if (_isClosure && Vector3.Distance(Verticies[Verticies.Count - 1], Verticies[0]) > _closureDistance)
                return false;

            return true;
        }

        private int GetLastTrackerIndex()
        {
            for (int i = 0; i < _trackers.Count; i++)
            {
                if (_trackers[i].CompareState(DrawState.Draw))
                    return i;
            }

            return -1;
        }

        private void InitLine(int index)
        {
            _trackers[index].Init();
            _trackers[index].ManualStartDraw(new InputData(_mouseButton, Verticies[Verticies.Count - 1]));

            _trackers[index].DrawFailed += CheckFailedLine;
            _trackers[index].CompleteDraw += CheckCompleteLine;
        }

        private void DisposeLine(int index)
        {
            _trackers[index].DrawFailed -= CheckFailedLine;
            _trackers[index].CompleteDraw -= CheckCompleteLine;

            _trackers[index].Dispose();
        }

        public override void Clear()
        {
            Verticies.Clear();

            for (int i = 0; i < _trackers.Count; i++)
            {
                DisposeLine(i);
                _trackers[i].Clear();
            }

            if (CompareState(DrawState.Draw))
                SetState(DrawState.None);
        }
    }
}