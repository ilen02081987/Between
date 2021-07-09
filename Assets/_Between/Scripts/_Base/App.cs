using Between.InputTracking;
using Between.Spells;
using Between.Utilities;
using UnityEngine;

namespace Between
{
    public class App : MonoBehaviourSingleton<App>
    {
        [SerializeField] private GameSettings _gameSettings;

        private void Awake()
        {
            _gameSettings.CreateInstance();
            new Player(FindObjectOfType<PlayerController>());
            InputLenghtCalculator.Init();
            new SpellsCollection().Init();
        }
    }
}