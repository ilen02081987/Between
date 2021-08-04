namespace Between.LevelObjects
{
    public class ManaBottle : InteractableObject
    {
        protected override void PerformOnInteract()
        {
            Player.Instance.ManaBottlesHolder.Add();
            Destroy();
        }

        public override void DestroyOnLoad() => Destroy();
    }
}