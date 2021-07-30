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
                int previousValue = _count;
                _count = value;

                if (_count != previousValue)
                    CountChanged?.Invoke();
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
            Count--;
        }
    }
}