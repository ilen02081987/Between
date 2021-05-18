using Between.Spells.Shield;
using RH.Utilities.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells
{
    public class SpellsCollection : MonoBehaviourSingleton<SpellsCollection>
    {
        private List<BaseSpell> _spells;

        private void Start()
        {
            _spells = new List<BaseSpell>();
            AddSpell(new ShieldSpell());
        }

        private void OnDestroy()
        {
            foreach (var spell in _spells)
                spell.Dispose();

            _spells.Clear();
        }

        public void AddSpell(BaseSpell spell)
        {
            if (HasSpell())
                throw new System.Exception($"[SpellsCollection] - Can't add spell {spell.GetType().Name}. It alreary added.");
            else
            {
                _spells.Add(spell);
                spell.Init();
            }

            bool HasSpell() => _spells.Find(x => x.GetType().Equals(spell.GetType())) != null;
        }
    }
}