using Between.Damage;
using Between.Teams;

namespace Between.Interfaces
{
    public interface IDamagable
    {
        Team Team { get; }
        void ApplyDamage(DamageItem damage);
    }
}