using Between.Enemies;
using Between.Saving;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Between.Spawning
{
    public class SpawnPoint : MonoBehaviour
    {
        public event Action OnSpawn;

        [SerializeField] private GameObject _spawnedObject;
        [SerializeField] private Transform _atPoint;

        private Vector3 _position => _atPoint != null ? _atPoint.position : transform.position;

        public GameObject Spawn()
        {
            OnSpawn?.Invoke();
            GameObject gameObject = Instantiate(_spawnedObject, _position, Quaternion.identity);

            if (gameObject.scene.buildIndex != LevelManager.Instance.SceneIndex)
                MoveToLevelScene(gameObject);

            ApplySavableId(gameObject);

            return gameObject;
        }

        private void MoveToLevelScene(GameObject gameObject)
        {
            var rootObjects = SceneManager.GetSceneByBuildIndex(LevelManager.Instance.SceneIndex).GetRootGameObjects();
            gameObject.transform.parent = rootObjects[0].transform;
        }

        private void ApplySavableId(GameObject to)
        {
            if (to.TryGetComponent<SavableObject>(out var savable))
                savable.Id = GetComponent<SavableObject>().Id;
        }
    }
}