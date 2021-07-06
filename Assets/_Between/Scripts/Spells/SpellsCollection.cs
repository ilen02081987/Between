using Between.Utilities;
using System.Collections.Generic;

namespace Between.Spells
{
    public class SpellsCollection : Singleton<SpellsCollection>
    {
        private Dictionary<SpellType, BaseSpell> _spells = new Dictionary<SpellType, BaseSpell>();

        public void Init()
        {
            _spells.Add(SpellType.Shield, new ShieldSpell());

            _spells.Add(SpellType.SmallProjectile, new ProjectileSpell("Projectile"
                , GameSettings.Instance.ProjectileSpellCooldown
                , GameSettings.Instance.ProjectileMinLenght
                , GameSettings.Instance.ProjectileMiddleLenght));

            _spells.Add(SpellType.BigProjectile, new ProjectileSpell("BigProjectile"
                , GameSettings.Instance.BigProjectileSpellCooldown
                , GameSettings.Instance.ProjectileMiddleLenght
                , GameSettings.Instance.ProjectileMaxLenght
                , GameSettings.Instance.BigProjectileCastDelay));

            _spells.Add(SpellType.MeteorRain, new MeteorRainSpell("Meteor"));

            foreach (KeyValuePair<SpellType, BaseSpell> pair in _spells)
                pair.Value.Init();
        }

        public BaseSpell GetSpell(SpellType spellType)
        {
            foreach (KeyValuePair<SpellType, BaseSpell> spellItem in _spells)
            {
                if (spellItem.Key == spellType)
                    return spellItem.Value;
            }

            return null;
        }

        public enum SpellType
        {
            Shield = 0,
            SmallProjectile,
            BigProjectile,
            MeteorRain
        }
    }
}