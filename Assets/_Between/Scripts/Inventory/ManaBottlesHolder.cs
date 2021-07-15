using Between.Mana;
using System;

namespace Between.Inventory
{
    public class ManaBottlesHolder
    {
        public event Action CountChanged;

        public int Count
        {
            get => _count;
            set
            {
                if (_count != value)
                    CountChanged?.Invoke();

                _count = value;
            }
        }

        private int _count;

        public void Add()
        {
            Count++;
        }

        public void Apply()
        {
            if (Count <= 0)
                return;

            Player.Instance.Mana.Add(GameSettings.Instance.ManaBottleValue);
        }
    }
}