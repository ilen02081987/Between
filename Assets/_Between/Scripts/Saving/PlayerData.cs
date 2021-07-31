using System;
using System.Collections.Generic;

namespace Between.Saving
{
    [Serializable]
    public class PlayerData
    {
        public int LevelSceneBuildIndex;
        public int LoadPointNumber;
        public List<int> ExistingGameObjects;

        public PlayerData(int buildIndex, int pointNumber, List<int> objects)
        {
            LevelSceneBuildIndex = buildIndex;
            LoadPointNumber = pointNumber;
            ExistingGameObjects = objects;
        }
    }
}