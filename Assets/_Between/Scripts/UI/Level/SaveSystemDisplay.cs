using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Between.Saving;
using Between.UI.Base;
using Between.Utilities;

namespace Between.UI.Level
{
    public class SaveSystemDisplay : UiElement
    {
        [SerializeField] private Text _tip;
        [SerializeField] private float _showTime;

        private bool _isShowing;

        public override void Init()
        {
            SaveSystem.DataSaved += ShowTip;
        }

        public override void Dispose()
        {
            SaveSystem.DataSaved -= ShowTip;
        }

        private void ShowTip()
        {
            if (!_isShowing)
                CoroutineLauncher.Start(ShowingTip());
        }

        private IEnumerator ShowingTip()
        {
            _isShowing = true;
            _tip.enabled = true;

            yield return new WaitForSeconds(_showTime);

            if (_tip == null)
                yield break;

            _isShowing = false;
            _tip.enabled = false;
        }
    }
}