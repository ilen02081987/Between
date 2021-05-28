using Between.Spells;
using Between.Utilities;

namespace Between
{
    public class App : MonoBehaviourSingleton<App>
    {
        private void Start()
        {
            new Player();
            new SpellsCollection();
        }
    }
}