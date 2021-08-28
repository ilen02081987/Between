using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Between.UI.Base
{
    [RequireComponent(typeof(Button))]
    public abstract class UiButton : MonoBehaviour
    {
        public bool Interactable
        {
            get => _button.interactable;
            set => _button.interactable = value;
        }

        protected bool isInitialized;

        private Button _button;

        public void AddListener(UnityAction action) => _button.onClick.AddListener(action);
        public void RemoveListener(UnityAction action) => _button.onClick.RemoveListener(action);

        public void RemoveAllListeners() => _button.onClick.RemoveAllListeners();

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PerformOnClick);

            Init();
            isInitialized = true;
        }

        private void OnDestroy()
        {
            if (_button != null)
                _button.onClick.RemoveListener(PerformOnClick);
        }

        protected virtual void Init() { }
        protected abstract void PerformOnClick();
    }
}