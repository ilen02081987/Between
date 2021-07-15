using UnityEngine;
using Between.InputTracking;
using Between.Spells;
using Between.Utilities;
using Between.UI;

namespace Between
{
    public class App : MonoBehaviourSingleton<App>
    {
        [SerializeField] private GameSettings _gameSettings;
        private GameOverlay _gameOverlay;

        private void Awake()
        {
            _gameSettings.CreateInstance();

            InitPlayer();

            InputLenghtCalculator.Init();
            new SpellsCollection().Init();

            InitGameOverlay();
        }

        private void OnDestroy()
        {
            _gameOverlay.Dispose();
        }

        private static void InitPlayer()
        {
            new Player(FindObjectOfType<PlayerController>());
            Player.Instance.Controller.InitDamagableObject();
        }

        private void InitGameOverlay()
        {
            _gameOverlay = FindObjectOfType<GameOverlay>();
            _gameOverlay.Init();
        }
    }
}