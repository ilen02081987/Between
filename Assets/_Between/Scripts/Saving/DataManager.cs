using Between.Saving;
using Between.Utilities;

namespace Between.Data
{
    public class DataManager : Singleton<DataManager>
    {
        public PlayerData SavedData;

        public DataManager()
        {
            if (SaveSystem.CanLoad)
                SavedData = SaveSystem.Load();
        }
    }
}