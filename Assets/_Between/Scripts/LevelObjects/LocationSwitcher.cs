using Between.SceneManagement;
using UnityEngine;

namespace Between.LevelObjects
{
    public class LocationSwitcher : LockableObject
    {
        [SerializeField] private int _nextSceneBuildIndex;

        protected override void InteractAfterUnlock()
        {
            SceneChanger.ChangeScene(LevelManager.Instance.SceneIndex, _nextSceneBuildIndex);
        }
    }
}