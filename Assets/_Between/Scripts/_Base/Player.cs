using Between.Inventory;
using Between.Mana;
using Between.Saving;
using Between.Utilities;

namespace Between
{
    public class Player : Singleton<Player>
    {
        public PlayerData Data { get; private set; }
        public ManaHolder Mana { get; private set; }
        public PlayerController Controller { get; private set; }
        public ManaBottlesHolder ManaBottles { get; private set; }

        public Player(PlayerController playerController)
        {
            InitData();
            InitPlayerController(playerController);
            InitMana();
            InitManaBottlesHolder();
        }

        private void InitData()
        {
            if (!SaveSystem.CanLoad())
                Data = new PlayerData().CreateDefault();
            else
                Data = SaveSystem.Load();

            SaveSystem.Save();
        }

        private void InitPlayerController(PlayerController playerController)
            => Controller = playerController;

        private void InitMana()
        {
            Mana = new ManaHolder(GameSettings.Instance.ManaMaxValue, GameSettings.Instance.ManaRecoveryPerSec);
            Mana.StartRecover();
        }

        private void InitManaBottlesHolder()
        {
            ManaBottles = new ManaBottlesHolder();
        }
    }
}