using UnityEngine;
using Between.InputTracking;

namespace Between.InputPainting
{
    public class InputPainter : MonoBehaviour
    {
        [SerializeField] private int _mouseButton;
        [SerializeField] private GameObject _prefab;

        private GameObject _currentPainter;

        private void Start()
        {
            InputHandler.StartDraw += CreatePainter;
            InputHandler.DrawCall += MovePainter;
            InputHandler.ForceEndDraw += DestroyPainter;
            InputHandler.EndDraw += DestroyPainter;
        }

        private void OnDestroy()
        {
            InputHandler.StartDraw -= CreatePainter;
            InputHandler.DrawCall -= MovePainter;
            InputHandler.ForceEndDraw -= DestroyPainter;
            InputHandler.EndDraw -= DestroyPainter;
        }

        private void CreatePainter(InputData screenPoint)
        {
            if (screenPoint.MouseButton != _mouseButton)
                return;

            if (_currentPainter != null)
                Destroy(_currentPainter);

            _currentPainter = Instantiate(_prefab, GetWorldPosition(screenPoint.Position), Quaternion.identity);
        }

        private void MovePainter(InputData screenPoint)
        {
            if (screenPoint.MouseButton != _mouseButton)
                return;

            if (_currentPainter != null)
                _currentPainter.transform.position = GetWorldPosition(screenPoint.Position);
        }

        private void DestroyPainter(InputData obj)
        {
            if (_currentPainter != null)
                Destroy(_currentPainter);
        }

        private Vector3 GetWorldPosition(Vector2Int from) => GameCamera.ScreenToWorldPoint(from);
    }
}