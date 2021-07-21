using Between.MainCharacter;
using Between.LevelObjects;
using Between.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Between.UI
{
    public class InteractionTip : UiElement
    {
        [SerializeField] private Text _title;

        private ObjectsInteractor _interactor;

        public override void Init()
        {
            _interactor = Player.Instance.ObjectsInteractor;

            _interactor.OnObjectEnter += EnableTip;
            _interactor.OnObjectExit += DisableTip;
        }

        public override void Dispose()
        {
            _interactor.OnObjectEnter += EnableTip;
            _interactor.OnObjectExit += DisableTip;
        }

        private void EnableTip(InteractableObject obj)
        {
            _title.gameObject.SetActive(true);
            _title.text = $"E - {obj.TipText}";
        }
        
        private void DisableTip()
        {
            _title.gameObject.SetActive(false);
        }
    }
}