using System;
using UnityEngine;

namespace Between.InputTracking.Trackers
{
    public abstract class BaseInputTracker
    {
        public event Action CompleteDraw;
        public event Action DrawFailed;
        public event Action CanCompleteDraw;

        protected DrawState State { get; private set; }
        protected int _mouseButton { get; private set; }

        public BaseInputTracker(int mouseButton)
        {
            _mouseButton = mouseButton;
        }

        public void Init()
        {
            InputHandler.StartDraw += StartDraw;
            InputHandler.DrawCall += DrawCall;
            InputHandler.EndDraw += EndDraw;
            InputHandler.ForceEndDraw += ForceEndDraw;
        }

        public void Dispose()
        {
            InputHandler.StartDraw -= StartDraw;
            InputHandler.DrawCall -= DrawCall;
            InputHandler.EndDraw -= EndDraw;
            InputHandler.ForceEndDraw -= ForceEndDraw;
        }

        protected abstract void Clear();

        private void StartDraw(InputData point)
        {
            if (point.MouseButton == _mouseButton && CompareState(DrawState.None))
            {
                SetState(DrawState.Draw);
                OnDrawStarted(point);
            }
        }

        private void DrawCall(InputData point)
        {
            if (point.MouseButton == _mouseButton && CompareState(DrawState.Draw))
                OnDrawCalled(point);
        }

        private void EndDraw(InputData point)
        {
            if (point.MouseButton == _mouseButton && CompareState(DrawState.Draw))
                OnDrawEnded(point);
        }

        private void ForceEndDraw(InputData point)
        {
            if (point.MouseButton == _mouseButton && CompareState(DrawState.Draw))
                OnDrawForceEnded(point);
        }

        protected abstract void OnDrawStarted(InputData obj);
        protected virtual void OnDrawCalled(InputData obj) { }
        protected virtual void OnDrawEnded(InputData obj) { }
        protected virtual void OnDrawForceEnded(InputData obj) { }


        #region STATE MACHINE

        protected void SetState(DrawState state)
        {
            if (CompareState(state))
                ThrowException($"Can't switch state {State}");

            State = state;
        }

        public bool CompareState(DrawState state) => State == state;

        public enum DrawState
        {
            None = 0,
            Draw
        }

        #endregion

        #region EVENTS METHODS

        protected void InvokeCompleteEvent() => CompleteDraw?.Invoke();
        protected void InvokeCanCompleteEvent() => CanCompleteDraw?.Invoke();
        protected void InvokeFailedEvent() => DrawFailed?.Invoke();

        #endregion
        
        protected void ThrowException(string message) => throw new Exception($"[{GetType().Name}] - {message}");
    }
}
