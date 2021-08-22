using Between.SceneManagement;
using System;
using UnityEngine;

namespace Between.LevelObjects
{
    public class LocationSwitcher : LockableObject
    {
        public event Action OnEnter;

        [SerializeField] private int _nextSceneBuildIndex;

        protected override void InteractAfterUnlock()
        {
            OnEnter?.Invoke();
            SceneChanger.ChangeScene(LevelManager.Instance.SceneIndex, _nextSceneBuildIndex);
        }
    }
}