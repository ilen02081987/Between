using UnityEngine;

namespace Between.SpellPainting
{
    public class LineRendererPainter : BasePainter
    {
        [SerializeField] private LineRenderer _rendererPrefab;

        private LineRenderer _currentRenderer;

        public override void Draw(Vector3 point)
        {
            if (_currentRenderer == null)
                CreateNewRenderer();

            var newPoint = _startPoint + point;

            _currentRenderer.positionCount++;
            _currentRenderer.SetPosition(_currentRenderer.positionCount - 1, newPoint);
        }

        public override void AddSpace()
        {
            _currentRenderer = null;
        }

        private void CreateNewRenderer() => _currentRenderer = Instantiate(_rendererPrefab, transform);
    }
}