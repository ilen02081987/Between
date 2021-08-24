using System.Collections;
using UnityEngine;

namespace Between.UI.Menu
{
    public class SelectBox : MonoBehaviour
    {
        [SerializeField] private SelectDifficultyButton[] _buttons;
        [SerializeField] private int _defaultSelectedButton;

        private IEnumerator Start()
        {
            PrepareButtons();
            yield return null;

            for (int i = 0; i < _buttons.Length; i++)
            {
                if (_buttons[i].IsSelected)
                    SelectButton(i);
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _buttons.Length; i++)
                _buttons[i].RemoveAllListeners();
        }

        private void PrepareButtons()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                int number = i;
                _buttons[i].AddListener(() => SelectButton(number));
            }
        }

        private void SelectButton(int number)
        {
            for (int i = 0; i < _buttons.Length; i++)
                _buttons[i].Interactable = i != number ? true : false;
        }
    }
}