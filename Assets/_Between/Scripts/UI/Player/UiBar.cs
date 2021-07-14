using UnityEngine;
using UnityEngine.UI;

namespace Between.UI
{
    [RequireComponent(typeof(Slider))]
    public abstract class UiBar : MonoBehaviour
    {
        private Slider _slider;

        public void Init()
        {
            _slider = GetComponent<Slider>();
            Run();
        }

        public void Dispose()
        {
            PerformOnDestroy();
        }

        protected abstract void Run();
        protected abstract void PerformOnDestroy();

        protected void UpdateBar(float value)
        {
            _slider.value = value;
        }
    }
}