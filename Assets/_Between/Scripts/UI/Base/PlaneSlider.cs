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
                _fill.localScale = new Vector3(_fill.localScale.x, _fill.localScale.y, value * _background.localScale.z);
            }
        }

        [SerializeField] private Transform _background;
        [SerializeField] private Transform _fill;

        private float _value;

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            //transform.LookAt(Player.Instance.Controller.transform);
        }

        public void Disable() => gameObject.SetActive(false);
    }
};