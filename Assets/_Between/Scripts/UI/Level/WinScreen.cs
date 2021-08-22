using UnityEngine;
using Between.LevelObjects;
using Between.SceneManagement;
using Between.UI.Base;
using System.Collections;

namespace Between.UI.Level
{
    public class WinScreen : UiScreen
    {
        [SerializeField] private LocationSwitcher _locationSwitcher;

        private WaitForSeconds _delay = new WaitForSeconds(.5f);
        private bool _readyToExit = false;

        private void Start()
        {
            _locationSwitcher.OnInteract += Enable;
        }

        protected override void PerformOnEnable()
        {
            _locationSwitcher.OnInteract -= Enable;
            StartCoroutine(DelayedEnableExit());
        }

        private IEnumerator DelayedEnableExit()
        {
            yield return _delay;
            _readyToExit = true;
        }

        private void Update()
        {
            if (Input.anyKeyDown && _readyToExit)
                SceneChanger.ChangeScene(LevelManager.Instance.SceneIndex, 1);
        }
    }
}