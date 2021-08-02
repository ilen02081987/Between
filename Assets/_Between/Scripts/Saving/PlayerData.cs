using System.Collections.Generic;
using UnityEngine;

namespace Between.Data
{
    public class PlayerData
    {
        public int LevelSceneBuildIndex;
        public int LoadPointNumber;
        public int ManaBottlesCount;

        public List<int> ExistingGameObjects;

        public PlayerData(int buildIndex, int pointNumber, List<int> objects)
        {
            LevelSceneBuildIndex = buildIndex;
            LoadPointNumber = pointNumber;
            ExistingGameObjects = objects;
            ManaBottlesCount = Player.Instance.ManaBottlesHolder.Count;
        }
    }
}