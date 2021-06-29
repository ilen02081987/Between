using Between.Utilities;
using System;
using System.Collections.Generic;

namespace Between.Spells
{
    public class SpellsCollection : Singleton<SpellsCollection>
    {
        private List<BaseSpell> _spells = new List<BaseSpell>
        {
            new ShieldSpell(),
            new ProjectileSpell("Projectile"),
            new MeteorRainSpell("Meteor")
        };

        public void Init()
        {
            foreach (BaseSpell spell in _spells)
                spell.Init();
        }

        public BaseSpell GetSpell(Type spellType) =>
            _spells.Find(spell => spell.GetType().Equals(spellType));
    }
}