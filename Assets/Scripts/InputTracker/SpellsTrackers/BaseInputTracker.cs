using System;

namespace Between.UserInput.Trackers
{
    public abstract class BaseInputTracker
    {
        public event Action CompleteDraw;
        public event Action DrawFailed;
        public event Action CanCompleteDraw;

        public abstract int MouseButton { get; }

        protected DrawState State { get; private set; }

        public BaseInputTracker() => Init();

        private void Init()
        {
            InputHandler.StartDraw += OnDrawStarted;
            InputHandler.DrawCall += OnDrawCalled;
            InputHandler.EndDraw += OnDrawEnded;
            InputHandler.ForceEndDraw += OnDrawForceEnded;
        }

        public void Dispose()
        {
            InputHandler.StartDraw += OnDrawStarted;
            InputHandler.DrawCall += OnDrawCalled;
            InputHandler.EndDraw += OnDrawEnded;
            InputHandler.ForceEndDraw += OnDrawForceEnded;
        }

        protected abstract void OnDrawStarted(InputData obj);
        protected abstract void OnDrawForceEnded(InputData obj);
        protected abstract void OnDrawEnded(InputData obj);
        protected abstract void OnDrawCalled(InputData obj);

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
