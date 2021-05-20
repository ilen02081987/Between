using RH.Utilities.Singleton;
using UnityEngine;

namespace Between
{
    public class App : MonoBehaviourSingleton<App>
    {
        private void Start()
        {
            new Player();
        }
    }
}