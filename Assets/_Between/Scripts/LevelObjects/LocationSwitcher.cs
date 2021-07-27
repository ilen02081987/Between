using UnityEngine;
using UnityEngine.SceneManagement;

namespace Between.LevelObjects
{
    public class LocationSwitcher : LockableObject
    {
        [SerializeField] private int _nextSceneBuildIndex;

        protected override void InteractAfterUnlock()
        {
            SceneManager.LoadScene(_nextSceneBuildIndex);
        }
    }
}