namespace Between.UI.Level
{
    public class DeathScreen : UiScreen
    {
        private void Start()
        {
            Player.Instance.Controller.OnDie += Enable;
        }

        protected override void PerformOnEnable()
        {
            if (Player.Instance != null)
                Player.Instance.Controller.OnDie -= Enable;
        }
    }
}