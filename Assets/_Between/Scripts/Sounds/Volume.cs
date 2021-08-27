using System;

namespace Between.Sounds
{
    public class Volume
    {
        public static event Action OnChanged;

        public static float Value
        {
            get => _value;
            set
            {
                if (_value != value)
                    OnChanged?.Invoke();

                _value = value;
            }
        }

        private static float _value = 1f;
    }
}