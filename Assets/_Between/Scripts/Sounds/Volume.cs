using System;

namespace Between.Sounds
{
    public class Volume
    {
        public static event Action OnValueChanged;

        public static float Value
        {
            get => _value;
            set
            {
                if (_value != value)
                    OnValueChanged?.Invoke();

                _value = value;
            }
        }

        private static float _value = 1f;
    }
}