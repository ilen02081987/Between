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
        [SerializeField] private GameOverlay _gameOverlay;

        private void Awake()
        {
            _gameSettings.CreateInstance();

            new Player(FindObjectOfType<PlayerController>());
            Player.Instance.Controller.InitDamagableObject();

            InputLenghtCalculator.Init();
            new SpellsCollection().Init();
            _gameOverlay.Init();
        }

        private void OnDestroy()
        {
            _gameOverlay.Dispose();
        }
    }
}