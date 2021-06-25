using Between.SpellsEffects.Projectile;
using Between.Teams;
using System.Collections.Generic;

namespace Between.Spells
{
    public class SpellsCollection
    {
        private List<BaseSpell> _spells = new List<BaseSpell>
        {
            new ShieldSpell(),
            new ProjectileSpell("Projectile"),
            new MeteorRainSpell("Meteor")
        };
    }
}