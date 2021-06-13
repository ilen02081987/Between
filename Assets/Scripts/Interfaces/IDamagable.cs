using Between.Teams;

namespace Between.Interfaces
{
    public interface IDamagable
    {
        Team Team { get; set; }
        void ApplyDamage(float damage);
    }
}