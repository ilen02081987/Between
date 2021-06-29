using Between.Saving;
using Between.Utilities;
using UnityEngine;

namespace Between
{
    public class Player : Singleton<Player>
    {
        public PlayerData Data { get; private set; }

        public Player()
        {
            if (!SaveSystem.CanLoad())
                Data = new PlayerData().CreateDefault();
            else
                Data = SaveSystem.Load();

            SaveSystem.Save();
        }
    }
}