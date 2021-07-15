using Between.UI.Base;
using UnityEngine;

namespace Between.UI
{
    public class GameOverlay : MonoBehaviour
    {
        [SerializeField] private UiElement[] _elements;

        public void Init()
        {
            foreach (var element in _elements)
                element.Init();
        }

        public void Dispose()
        {
            foreach (var element in _elements)
                element.Dispose();
        }
    }
}