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
        [SerializeField] private CheckPoint[] _checkPoints;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        private GameOverlay _gameOverlay;

        private void Awake() => Init();
        private void OnDestroy() => Dispose();

        public void Init()
        {
            InitSettings();
            InitCheckPoints();
            InitPlayer();
            AttachCameraToPlayer();
            InputLenghtCalculator.Init();
            new SpellsCollection().Init();
            
            InitGameOverlay();
        }

        private void InitCheckPoints()
        {
            for (int i = 0; i < _checkPoints.Length; i++)
                _checkPoints[i].AttachNumber(i);
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