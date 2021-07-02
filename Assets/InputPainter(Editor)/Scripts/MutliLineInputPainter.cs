using Between;
using Between.UserInput;
using UnityEngine;

public class MutliLineInputPainter : MonoBehaviour
{
    private LineRenderer _currentRenderer;

    private void Start()
    {
        InputHandler.DrawCall += Draw;
        InputHandler.EndDraw += AddSpace;
    }

    private void Draw(InputData obj)
    {
        if (_currentRenderer == null)
            CreateNewRenderer();

        var newPoint = GameCamera.ScreenToWorldPoint(InputHandler.MousePosition);

        _currentRenderer.positionCount++;
        _currentRenderer.SetPosition(_currentRenderer.positionCount - 1, newPoint);
    }

    private void AddSpace(InputData obj)
    {
        _currentRenderer = null;
    }

    private void CreateNewRenderer()
    {
        var gameObject = new GameObject("Renderer", typeof(LineRenderer));
        gameObject.transform.parent = transform;

        _currentRenderer = gameObject.GetComponent<LineRenderer>();
        _currentRenderer.positionCount = 0;
        _currentRenderer.widthMultiplier *= .5f;
    }
}
