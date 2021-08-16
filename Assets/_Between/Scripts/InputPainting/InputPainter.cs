using UnityEngine;
using Between.InputTracking;
using Between.Spells;

namespace Between.InputPainting
{
    public class InputPainter : MonoBehaviour
    {
        [SerializeField] private int _mouseButton;
        [SerializeField] private InputPainterEffect _prefab;
        [SerializeField] private Color _notRecognizedColor;
        [SerializeField] private Color _notEnoughManaColor;

        private InputPainterEffect _currentPainter;

        private void Start()
        {
            InputHandler.StartDraw += CreatePainter;
            InputHandler.DrawCall += MovePainter;
            InputHandler.ForceEndDraw += DestroyPainter;

            BaseSpell.SpellCasted += PerformOnSpellCasted;
            BaseSpell.NotEnoughMana += PerformOnSpellNotEnoughMana;
            BaseSpell.NotRecognizeSpell += PerformOnSpellNotRecognized;
        }

        private void OnDestroy()
        {
            InputHandler.StartDraw -= CreatePainter;
            InputHandler.DrawCall -= MovePainter;
            InputHandler.ForceEndDraw -= DestroyPainter;
            
            BaseSpell.SpellCasted -= PerformOnSpellCasted;
            BaseSpell.NotEnoughMana -= PerformOnSpellNotEnoughMana;
            BaseSpell.NotRecognizeSpell -= PerformOnSpellNotRecognized;
        }

        private void CreatePainter(InputData screenPoint)
        {
            if (screenPoint.MouseButton != _mouseButton)
                return;

            if (_currentPainter != null)
                Destroy(_currentPainter.gameObject);

            _currentPainter = Instantiate(_prefab, GetWorldPosition(screenPoint.Position), Quaternion.identity);
        }

        private void MovePainter(InputData screenPoint)
        {
            if (screenPoint.MouseButton != _mouseButton)
                return;

            if (_currentPainter != null)
                _currentPainter.transform.position = GetWorldPosition(screenPoint.Position);
        }

        private void PerformOnSpellCasted()
        {
            DestroyPainter(default);
        }

        private void PerformOnSpellNotRecognized() => ChangePainterColor(_notRecognizedColor);
        private void PerformOnSpellNotEnoughMana() => ChangePainterColor(_notEnoughManaColor);

        private void DestroyPainter(InputData obj)
        {
            if (_currentPainter != null)
                Destroy(_currentPainter.gameObject);
        }

        private void ChangePainterColor(Color to)
        {
            if (_currentPainter != null)
            {
                _currentPainter.ChangeTrailColor(to);
                var painterToRemove = _currentPainter;
                _currentPainter = null;

                Destroy(painterToRemove.gameObject, 1.5f);
            }
        }

        private Vector3 GetWorldPosition(Vector2Int from) => GameCamera.ScreenToWorldPoint(from);
    }
}