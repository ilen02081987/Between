using Between.UserInput.Trackers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells
{
    public abstract class BaseSpell
    {
        protected abstract BaseInputTracker tracker { get; }

        public BaseSpell() => Init();

        private void Init()
        {
            tracker.CanCompleteDraw += OnCanCompleteDraw;
            tracker.CompleteDraw += OnCompleteDraw;
            tracker.DrawFailed += OnDrawFailed;
        }

        public void Dispose()
        {
            tracker.CanCompleteDraw -= OnCanCompleteDraw;
            tracker.CompleteDraw -= OnCompleteDraw;
        }

        protected abstract void OnCompleteDraw();
        protected abstract void OnCanCompleteDraw();
        protected abstract void OnDrawFailed();
    }
}