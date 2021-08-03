using Between.Saving;
using Between.Utilities;

namespace Between.Data
{
    public class DataManager : Singleton<DataManager>
    {
        public PlayerData SavedData;
        public bool HasData => SavedData != null;

        public DataManager() => Load();

        public void Load()
        {
            if (SaveSystem.CanLoad)
                SavedData = SaveSystem.Load();
        }

        public void ClearSave()
        {
            SaveSystem.ClearSave();
            SavedData = null;
        }
    }
}