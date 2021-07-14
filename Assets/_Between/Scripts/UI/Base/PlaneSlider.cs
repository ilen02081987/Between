using UnityEngine;

namespace Between.UI.Base
{
    public class PlaneSlider : MonoBehaviour
    {
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                _fill.localScale = new Vector3()
            }
        }

        [SerializeField] private Transform _background;
        [SerializeField] private Transform _fill;

        private float _value;

        private void Update()
        {
            
        }
    }
}