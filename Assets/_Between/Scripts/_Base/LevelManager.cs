using UnityEngine;
using Cinemachine;
using Between.UI;
using Between.InputTracking;
using Between.Spells;
using Between.LevelObjects;
using Between.Data;

namespace Between
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public int SceneIndex => Instance.gameObject.scene.buildIndex;

        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private CheckPoint[] _checkPoints;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        private GameOverlay _gameOverlay;

        private void Awake() => Init();
        private void OnDestroy() => Dispose();

        public void Init()
        {
            Instance = this;

            InitSettings();
            InitCheckPoints();
            InitPlayer();
            AttachCameraToPlayer();
            InputLenghtCalculator.Init();
            new SpellsCollection().Init();
            
            InitGameOverlay();
        }

        public void Dispose()
        {
            InputLenghtCalculator.Dispose();
            Player.DestroyInstance();
            SpellsCollection.DestroyInstance();
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
            if (DataManager.Instance?.SavedData == null)
                _checkPoints[0].LoadPlayer();
            else
            {
                var pointNumber = DataManager.Instance.SavedData.LoadPointNumber;
                _checkPoints[pointNumber].LoadPlayer();

                for (int i = 0; i < pointNumber; i++)
                    _checkPoints[i].Clear();
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
    }
}