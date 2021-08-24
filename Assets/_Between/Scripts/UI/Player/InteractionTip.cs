using UnityEngine;
using UnityEngine.UI;
using Between.MainCharacter;
using Between.LevelObjects;
using Between.UI.Base;

namespace Between.UI
{
    public class InteractionTip : UiElement
    {
        [SerializeField] private Transform _textsParent;
        [SerializeField] private Text _name;
        [SerializeField] private Text _tip;

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
            if (_textsParent == null)
                return;

            _textsParent.gameObject.SetActive(true);

            _name.text = obj.Name;
            _tip.text = obj.TipText;
        }
        
        private void DisableTip()
        {
            _textsParent.gameObject.SetActive(false);
        }
    }
}