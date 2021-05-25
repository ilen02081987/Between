using System;

namespace Between.UserInput.Trackers
{
    public abstract class BaseInputTracker
    {
        public event Action CompleteDraw;
        public event Action DrawFailed;
        public event Action CanCompleteDraw;

        protected abstract int MouseButton { get; }

        protected DrawState State { get; private set; }

        public BaseInputTracker() => Init();

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

        private void StartDraw(InputData point)
        {
            if (point.MouseButton == MouseButton && CompareState(DrawState.None))
                OnDrawStarted(point);
        }

        private void DrawCall(InputData point)
        {
            if (point.MouseButton == MouseButton && CompareState(DrawState.Draw))
                OnDrawCalled(point);
        }

        private void EndDraw(InputData point)
        {
            if (point.MouseButton == MouseButton && CompareState(DrawState.Draw))
                OnDrawEnded(point);
        }

        private void ForceEndDraw(InputData point)
        {
            if (point.MouseButton == MouseButton && CompareState(DrawState.Draw))
                OnDrawForceEnded(point);
        }

        protected abstract void OnDrawStarted(InputData obj);
        protected abstract void OnDrawCalled(InputData obj);
        protected abstract void OnDrawEnded(InputData obj);
        protected abstract void OnDrawForceEnded(InputData obj);

        #region STATE MACHINE

        protected void SetState(DrawState state)
        {
            if (CompareState(state))
                ThrowException($"Can't switch state {State}");

            State = state;
        }

        protected bool CompareState(DrawState state) => State == state;

        protected enum DrawState
        {
            None = 0,
            Draw
        }

        #endregion

        #region INVOKE EVENTS METHODS

        protected void InvokeCompleteEvent() => CompleteDraw?.Invoke();
        protected void InvokeCanCompleteEvent() => CanCompleteDraw?.Invoke();
        protected void InvokeFailedEvent() => DrawFailed?.Invoke();

        #endregion
        
        protected void ThrowException(string message) => throw new Exception($"[{GetType().Name}] - {message}");
    }
}
