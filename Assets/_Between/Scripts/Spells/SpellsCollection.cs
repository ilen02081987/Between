using RH.Utilities.SingletonAccess;
using System.Collections.Generic;

namespace Between.Spells
{
    public class SpellsCollection : Singleton<SpellsCollection>
    {
        private Dictionary<SpellType, BaseSpell> _spells = new Dictionary<SpellType, BaseSpell>();

        public void Init()
        {
            AddSpells();
            InitSpells();
        }

        protected override void PrepareToDestroy()
        {
            foreach (KeyValuePair<SpellType, BaseSpell> pair in _spells)
                pair.Value.Dispose();
        }

        private void AddSpells()
        {
            _spells.Add(SpellType.Shield, new ShieldSpell());
            _spells.Add(SpellType.SmallProjectile, new ProjectileSpell("Projectile"));
            _spells.Add(SpellType.MeteorRain, new MeteorRainSpell("Meteor"));
            _spells.Add(SpellType.Heal, new HealingSpell());
            _spells.Add(SpellType.FailedTracker, new FailedTrackerSpell());
        }

        private void InitSpells()
        {
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
            MeteorRain,
            Heal,
            FailedTracker
        }
    }
}