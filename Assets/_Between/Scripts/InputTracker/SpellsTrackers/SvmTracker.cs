using Between.Extensions;
using Between.SpellRecognition;
using System.Collections.Generic;
using UnityEngine;

namespace Between.UserInput.Trackers
{
    public class SvmTracker : BaseInputTracker
    {
        public List<Vector2Int> DrawPoints { get; private set; } = new List<Vector2Int>();

        private static Svm _svm;
        private readonly SpellFigure _figure;

        public SvmTracker(SpellFigure figure) : base(0) 
        {
            if (_svm == null)
                _svm = new Svm();

            _figure = figure;
        }

        protected override void Clear()
        {
            DrawPoints.Clear();
        }

        protected override void OnDrawStarted(InputData input)
        {
            DrawPoints.Add(input.Position.ToVector2Int());
        }

        protected override void OnDrawCalled(InputData input)
        {
            DrawPoints.Add(input.Position.ToVector2Int());
        }

        protected override void OnDrawEnded(InputData input)
        {
            if (_figure == _svm.Recognize(DrawPoints))
                InvokeCompleteEvent();

            Clear();
        }
    }
}