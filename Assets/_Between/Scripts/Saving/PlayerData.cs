namespace Between.Data
{
    public class PlayerData
    {
        public int LevelSceneBuildIndex;
        public int LoadPointNumber;

        public PlayerData(int buildIndex, int pointNumber)
        {
            LevelSceneBuildIndex = buildIndex;
            LoadPointNumber = pointNumber;
        }
    }
}