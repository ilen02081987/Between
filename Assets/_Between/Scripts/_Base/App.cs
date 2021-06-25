using Between.Spells;
using Between.Utilities;
using UnityEngine;

namespace Between
{
    public class App : MonoBehaviourSingleton<App>
    {
        [SerializeField] private GameSettings _gameSettings;

        private void Start()
        {
            _gameSettings.CreateInstance();

            new Player();
            new SpellsCollection();
        }
    }
}