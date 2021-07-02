using Between;
using UnityEngine;

public class LineRendererPainter : MonoBehaviour
{
    [SerializeField] private LineRenderer _rendererPrefab;

    private LineRenderer _currentRenderer;
    private Vector3 _startPoint;

    public void AddStartPoint(Vector3 position)
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
