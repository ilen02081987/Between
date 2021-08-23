using UnityEngine;
using UnityEngine.UI;
using Between.Sounds;

namespace Between.UI.Menu
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(ChangeMainVolume);
            _slider.value = Volume.Value;
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(ChangeMainVolume);
        }

        private void ChangeMainVolume(float value)
        {
            Volume.Value = value;
        }
    }
}