using UnityEngine;

namespace Between.UI
{
    public class GameOverlay : MonoBehaviour
    {
        [SerializeField] private UiBar[] _bars;
        [SerializeField] private SpellColldownDisplay[] _spellsDisplays;

        public void Init()
        {
            foreach (var bar in _bars)
                bar.Init();

            foreach (var spellDisplay in _spellsDisplays)
                spellDisplay.Init();
        }

        public void Dispose()
        {
            foreach (var bar in _bars)
                bar.Dispose();

            foreach (var spellDisplay in _spellsDisplays)
                spellDisplay.Dispose();
        }
    }
}