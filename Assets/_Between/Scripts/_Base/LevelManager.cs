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
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private LoadPoint[] _loadPoints;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        private GameOverlay _gameOverlay;

        private void Awake() => Init();
        private void OnDestroy() => Dispose();

        public void Init()
        {
            InitSettings();

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
        
        private void InitSettings()
        {
            _gameSettings.CreateInstance();
        }

        private void InitPlayer()
        {
            if (DataManager.Instance.SavedData == null)
                _loadPoints[0].LoadPlayer();
            else
                _loadPoints[DataManager.Instance.SavedData.LoadPointNumber].LoadPlayer();
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