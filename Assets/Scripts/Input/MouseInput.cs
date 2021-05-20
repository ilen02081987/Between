using System;
using System.Collections;
using UnityEngine;

namespace Between.UserInput
{
    public class MouseInput : MonoBehaviour
    {
        public static event Action<int, Vector3> OnStartDraw;
        public static event Action<int, Vector3> OnDrawCall;
        public static event Action<int, Vector3> OnDrawPause;
        public static event Action<int, Vector3> OnFinishDraw;

        private static readonly float _trackDelay = .1f;
        private static readonly float _drawTreshhold = 10f;
        private static float _drawTreshholdSqr => Mathf.Pow(_drawTreshhold, 2);

        private Vector3 _mousePosition => Input.mousePosition;

        private State _state;

        private void Update()
        {
            CheckMouseInput(0);
            CheckMouseInput(1);
        }

        public void CheckMouseInput(int button)
        {
            if (Input.GetMouseButtonDown(button) && CompareState(State.None))
            {
                SetState(State.Draw);
                OnStartDraw?.Invoke(button, _mousePosition);
                StartCoroutine(TrackMouse(button));
            }
        }

        private void FinishInput(int button)
        {
            SetState(State.None);
            OnFinishDraw?.Invoke(button, _mousePosition);
        }

        private IEnumerator TrackMouse(int button)
        {
            Vector3 previousPosition = _mousePosition;

            yield return new WaitForSeconds(_trackDelay);

            while (Input.GetMouseButton(button))
            {
                if (CanSetPause())
                    SetPause();
                else if (CanDraw())
                    Draw();

                yield return new WaitForSeconds(_trackDelay);
            }

            FinishInput(button);

            bool CanSetPause() => _mousePosition == previousPosition && !CompareState(State.Pause);
            bool CanDraw() => Vector3.SqrMagnitude(_mousePosition - previousPosition) > _drawTreshholdSqr;

            void SetPause()
            {
                SetState(State.Pause);
                OnDrawPause?.Invoke(button, _mousePosition);
            }

            void Draw()
            {
                TrySetState(State.Draw);
                previousPosition = _mousePosition;

                OnDrawCall?.Invoke(button, _mousePosition);
            }
        }

        private void TrySetState(State state)
        {
            if (!CompareState(state))
                SetState(state);
        }

        private void SetState(State state)
        {
            if (_state == state)
                throw new System.Exception($"[MouseInput] - Can't change state from {_state} to {state}");

            _state = state;
        }

        private bool CompareState(State state) => _state == state;

        private enum State { None = 0, Draw, Pause }
    }
}