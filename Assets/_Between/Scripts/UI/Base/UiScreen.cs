using UnityEngine;

namespace Between.UI.Base
{
    public abstract class UiScreen : MonoBehaviour
    {
        protected bool isEnabled => root.activeSelf;

        [SerializeField] protected GameObject root;

        protected void Enable()
        {
            root.SetActive(true);
            PerformOnEnable();
        }

        protected void Disable()
        {
            root.SetActive(false);
            PerformOnDisable();
        }

        protected virtual void PerformOnEnable() { }
        protected virtual void PerformOnDisable() { }
    }
}