using UnityEngine;
using UnityEngine.UI;

namespace Between.UI.Base
{
    [RequireComponent(typeof(Slider))]
    public abstract class UiBar : UiElement
    {
        private Slider _slider;

        public override void Init()
        {
            _slider = GetComponent<Slider>();
            Run();
        }

        public override void Dispose()
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