using UnityEngine;
using UnityEngine.UI;
using Between.Spells;
using Between.UI.Base;

namespace Between.UI
{
    public class SpellColldownDisplay : UiElement
    {
        [SerializeField] private SpellsCollection.SpellType _spellType;

        private Image _image;
        private BaseSpell _spell;

        public override void Init()
        {
            _image = GetComponent<Image>();

            _spell = SpellsCollection.Instance.GetSpell(_spellType);

            _spell.CooldownStarted += EmptiedDisplay;
            _spell.CooldownUpdated += UpdateDisplay;
            _spell.CooldownFinished += FillDisplay;
        }

        public override void Dispose()
        {
            _spell.CooldownUpdated -= UpdateDisplay;
        }

        private void EmptiedDisplay()
        {
            _image.fillAmount = 0f;
        }

        private void FillDisplay()
        {
            _image.fillAmount = 1f;
        }
        
        private void UpdateDisplay(float value)
        {
            _image.fillAmount = value;
        }
    }
}