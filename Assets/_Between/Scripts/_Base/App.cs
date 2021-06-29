using Between.Spells;
using Between.Utilities;
using UnityEngine;

namespace Between
{
    public class App : MonoBehaviourSingleton<App>
    {
        public PlayerController PlayerController;

        [SerializeField] private GameSettings _gameSettings;

        private void Awake()
        {
            _gameSettings.CreateInstance();

            new Player();
            new SpellsCollection().Init();
        }
    }
}