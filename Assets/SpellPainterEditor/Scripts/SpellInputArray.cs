using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpellPainterEditor
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
            RemoveLastSpace();
            DecreaseToMinPoint();

            void RemoveLastSpace()
            {
                if (Inputs[Inputs.Count - 1].Type == SpellInputType.Space)
                    Inputs.RemoveAt(Inputs.Count - 1);
            }

            void DecreaseToMinPoint()
            {
                var minPoint = FindMinPoint();

                foreach (SpellInput spellInput in AllPoints)
                    spellInput.Point -= minPoint;
            }
        }

        public void Compress(float value)
        {
            foreach (SpellInput spellInput in AllPoints)
                spellInput.Point /= value;
        }

        private Vector3 FindMinPoint()
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;

            foreach (SpellInput spellInput in AllPoints)
            {
                if (spellInput.Point.x < minX)
                    minX = spellInput.Point.x;

                if (spellInput.Point.y < minY)
                    minY = spellInput.Point.y;
            }

            return new Vector3(minX, minY);
        }

        private List<SpellInput> AllPoints => Inputs.FindAll(x => x.Type == SpellInputType.Dot);
    }

    [Serializable]
    public class SpellInput
    {
        public SpellInputType Type;
        public Vector3 Point;

        public SpellInput(SpellInputType type, Vector3 point)
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