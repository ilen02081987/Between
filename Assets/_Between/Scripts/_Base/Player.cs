using Between.Inventory;
using Between.MainCharacter;
using Between.Mana;
using Between.Utilities;

namespace Between
{
    public class Player : Singleton<Player>
    {
        public ManaHolder Mana { get; private set; }
        public PlayerController Controller { get; private set; }
        public ManaBottlesHolder ManaBottlesHolder { get; private set; }
        public ObjectsInteractor ObjectsInteractor { get; private set; }
        public ManaBottlesUser ManaBottlesUser { get; private set; }

        public Player(PlayerController playerController)
        {
            InitPlayerController(playerController);
            InitMana();
            InitManaBottlesHolder();
            InitObjectsInteractor(playerController);
            InitManaBottlesUser(playerController, ManaBottlesHolder);
        }

        private void InitPlayerController(PlayerController playerController)
        {
            Controller = playerController;
            Controller.SetMaxHealth(GameSettings.Instance.PlayerHealth);
            Controller.InitDamagableObject();
        }

        private void InitMana()
        {
            Mana = new ManaHolder(GameSettings.Instance.ManaMaxValue, GameSettings.Instance.ManaRecoveryPerSec);
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