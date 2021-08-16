using Between;
using Between.InputTracking;
using UnityEngine;

namespace CV.Editor
{
    [RequireComponent(typeof(LineRenderer))]
    public class RendererInputPainter : MonoBehaviour
    {
        [SerializeField] private int _mouseButton = 0;

        private LineRenderer _lineRenderer;
        private bool _isDraw = false;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            TryClear();
        }

        void Update()
        {
            UpdateState();
            UpdateRenderer();
        }

        private void UpdateState()
        {
            if (Input.GetMouseButtonDown(_mouseButton))
                _isDraw = true;
            else if (Input.GetMouseButtonUp(_mouseButton))
                _isDraw = false;
        }

        private void UpdateRenderer()
        {
            if (_isDraw)
                AddPoint();
            else
                TryClear();
        }

        private void AddPoint()
        {
            var lastPoint = _lineRenderer.positionCount > 0 ? 
                _lineRenderer.GetPosition(_lineRenderer.positionCount - 1) : Vector3.zero;
            var newPoint = GameCamera.ScreenToWorldPoint(InputHandler.MousePosition);

            if (lastPoint.Equals(newPoint))
                return;

            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newPoint);
        }

        private void TryClear()
        {
            if (_lineRenderer.positionCount > 0)
                _lineRenderer.positionCount = 0;
        }
    }
}