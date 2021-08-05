using UnityEngine;
using Cinemachine;
using Between.UI;
using Between.InputTracking;
using Between.Spells;
using Between.LevelObjects;
using Between.Data;
using Between.Saving;
using Between.Sounds;

namespace Between
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public int SceneIndex => Instance.gameObject.scene.buildIndex;

        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private CheckPoint[] _checkPoints;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private SpellsSoundsManager _spellsSounds;
        
        private GameOverlay _gameOverlay;

        private void Awake() => Init();
        private void OnDestroy() => Dispose();

        public void Init()
        {
            Instance = this;

            DataManager.Instance.Load();
            InitSettings();
            InitCheckPoints();
            InitPlayer();
            InitLevel();
            AttachCameraToPlayer();
            InputLenghtCalculator.Init();
            new SpellsCollection().Init();
            InitGameOverlay();
            _spellsSounds.Init();
        }

        
        public void Dispose()
        {
            InputLenghtCalculator.Dispose();
            DisposePlayer();
            Player.DestroyInstance();
            SpellsCollection.DestroyInstance();
            _gameOverlay?.Dispose();
            _spellsSounds.Dispose();
        }

        private void InitCheckPoints()
        {
            for (int i = 0; i < _checkPoints.Length; i++)
                _checkPoints[i].AttachNumber(i);
        }

        private void InitSettings()
        {
            _gameSettings.CreateInstance();
        }

        private void InitPlayer()
        {
            if (!DataManager.Instance.HasData || DataManager.Instance.SavedData.LevelSceneBuildIndex != SceneIndex)
                _checkPoints[0].LoadPlayer();
            else
            {
                var pointNumber = DataManager.Instance.SavedData.LoadPointNumber;
                _checkPoints[pointNumber].LoadPlayer();
            }
        }

        private void DisposePlayer()
        {
            if (Player.Instance.Controller != null)
                Destroy(Player.Instance.Controller.gameObject);
        }

        private void InitLevel()
        {
            if (DataManager.Instance.HasData)
            {
                var existingsObjects = DataManager.Instance.SavedData.ExistingGameObjects;
                var savableObjects = FindObjectsOfType<SavableObject>();

                foreach (SavableObject savableObject in savableObjects)
                {
                    if (!existingsObjects.Contains(savableObject.Id))
                        RemoveGameObject(savableObject.gameObject);
                }
            }
        }

        private void AttachCameraToPlayer()
        {
            _virtualCamera.Follow = Player.Instance.Controller.transform;
        }

        private void InitGameOverlay()
        {
            _gameOverlay = FindObjectOfType<GameOverlay>();
            _gameOverlay?.Init();
        }

        private void RemoveGameObject(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<InteractableObject>(out var interactable))
                interactable.DestroyOnLoad();
            else
                Destroy(gameObject);
        }
    }
}