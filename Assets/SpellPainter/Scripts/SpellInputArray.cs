using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpellPainter
{
    [Serializable]
    public class SpellInputArray
    {
        public List<SpellInput> Inputs;

        public void AddInput(SpellInput spellInput)
        {
            if (Inputs == null)
                Inputs = new List<SpellInput>();

            Inputs.Add(spellInput);
        }

        public void ConvertDirtyData()
        {
            if (Inputs[Inputs.Count].Type == SpellInputType.Space)
                Inputs.RemoveAt(Inputs.Count);  
        }
    }

    [Serializable]
    public class SpellInput
    {
        public SpellInputType Type;
        public Vector2Int Point;

        public SpellInput(SpellInputType type, Vector2Int point)
        {
            Type = type;
            Point = point;
        }
    }

    public enum SpellInputType
    {
        Dot = 0,
        Space
    }
}