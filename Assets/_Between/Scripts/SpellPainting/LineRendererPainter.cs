using Between;
using UnityEngine;

namespace Between.SpellPainting
{
    public class LineRendererPainter : MonoBehaviour, IPainter
    {
        [SerializeField] private LineRenderer _rendererPrefab;

        private LineRenderer _currentRenderer;
        private Vector3 _startPoint;

        public void Init(Vector3 position)
        {
            _startPoint = position;
        }

        public void Draw(Vector3 point)
        {
            if (_currentRenderer == null)
                CreateNewRenderer();

            var newPoint = _startPoint + point;

            _currentRenderer.positionCount++;
            _currentRenderer.SetPosition(_currentRenderer.positionCount - 1, newPoint);
        }

        public void AddSpace()
        {
            _currentRenderer = null;
        }

        private void CreateNewRenderer()
        {
            _currentRenderer = MonoBehaviour.Instantiate(_rendererPrefab, transform);
        }
    }

    public class TrailPainter : MonoBehaviour, IPainter
    {
        public void Init(Vector3 point)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(Vector3 point)
        {
            throw new System.NotImplementedException();
        }

        public void AddSpace()
        {
            
        }
    }
}