using Between.UserInput;
using UnityEngine;

namespace Between.Spells
{
    public abstract class BaseSpell
    {
        protected abstract string _painterName { get; }

        protected virtual void InitSpell() { }
        protected virtual void DisposeSpell() { }
        protected abstract void StartDraw(Vector3 point);
        protected abstract void Draw(Vector3 point);
        protected abstract void PauseDraw(Vector3 point);
        protected abstract void FinishDraw(Vector3 point);

        public readonly int ButtonNumber;

        private SpellPainter _prefab;
        private State _state;

        public BaseSpell(int buttonNumber)
        {
            ButtonNumber = buttonNumber;
        }

        public void Init()
        {
            MouseInput.OnStartDraw += OnStartDraw;
            MouseInput.OnDrawCall += OnDrawCall;
            MouseInput.OnDrawPause += OnDrawPause;
            MouseInput.OnFinishDraw += OnFinishDraw;

            InitSpell();
        }

        public void Dispose()
        {
            MouseInput.OnStartDraw -= OnStartDraw;
            MouseInput.OnDrawCall -= OnDrawCall;
            MouseInput.OnDrawPause -= OnDrawPause;
            MouseInput.OnFinishDraw -= OnFinishDraw;

            DisposeSpell();
        }

        private void OnStartDraw(int button, Vector3 point)
        {
            if (button == ButtonNumber)
            {
                CreatePainter();
                StartDraw(point);
            }
        }

        private void OnDrawCall(int button, Vector3 point)
        {
            if (button == ButtonNumber)
                Draw(point);
        }

        private void OnDrawPause(int button, Vector3 point)
        {
            if (button == ButtonNumber)
                PauseDraw(point);
        }

        private void OnFinishDraw(int button, Vector3 point)
        {
            if (button == ButtonNumber)
                FinishDraw(point);
        }

        private void CreatePainter()
        {
            if (_prefab == null)
                _prefab = Resources.Load<SpellPainter>(_painterName);

            var painter = MonoBehaviour.Instantiate<SpellPainter>(_prefab);
            painter.Init(ButtonNumber, Input.mousePosition);
        }

        protected void SetState(State state)
        {
            if (_state == state)
                throw new System.Exception($"[{GetType().Name} - can't change state from {_state} to {state}]");

            _state = state;
        }

        protected bool CompareState(State state) => _state == state;

        protected enum State
        {
            None = 0, Draw, Cooldown
        }
    }
}