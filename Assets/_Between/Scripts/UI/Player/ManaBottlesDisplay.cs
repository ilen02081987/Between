using Between.Inventory;
using Between.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Between.UI
{
    public class ManaBottlesDisplay : UiElement
    {
        [SerializeField] private Text _count;

        private ManaBottlesHolder _manaBottlesHolder;

        public override void Init()
        {
            _manaBottlesHolder = Player.Instance.ManaBottlesHolder;
            _manaBottlesHolder.CountChanged += UpdateValue;
            UpdateValue();
        }

        public override void Dispose()
        {
            _manaBottlesHolder.CountChanged -= UpdateValue;
        }

        private void UpdateValue()
        {
            _count.text = _manaBottlesHolder.Count.ToString();
        }
    }
}