using Between.Inventory;
using Between.MainCharacter;
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
        public ManaBottlesHolder ManaBottlesHolder { get; private set; }
        public ObjectsInteractor ObjectsInteractor { get; private set; }
        public ManaBottlesUser ManaBottlesUser { get; private set; }

        public Player(PlayerController playerController)
        {
            InitData();
            InitPlayerController(playerController);
            InitMana();
            InitManaBottlesHolder();
            InitObjectsInteractor(playerController);
            InitManaBottlesUser(playerController, ManaBottlesHolder);
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
            ManaBottlesHolder = new ManaBottlesHolder();
        }

        private void InitObjectsInteractor(PlayerController playerController)
        {
            ObjectsInteractor = playerController.GetComponent<ObjectsInteractor>();
        }
        
        private void InitManaBottlesUser(PlayerController playerController, ManaBottlesHolder holder)
        {
            ManaBottlesUser = playerController.GetComponent<ManaBottlesUser>();
            ManaBottlesUser.Init(holder);
        }
    }
}