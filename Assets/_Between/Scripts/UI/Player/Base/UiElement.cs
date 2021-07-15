using UnityEngine;

namespace Between.UI.Base
{
    public abstract class UiElement : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Dispose();
    }
}