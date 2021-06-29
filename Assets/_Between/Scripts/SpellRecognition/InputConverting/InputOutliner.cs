using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellRecognition.InputConversion
{
    public class InputOutliner
    {
        private static Vector2Int[] _addingVectors = new Vector2Int[]
            {
                new Vector2Int(0, 1),
                new Vector2Int(1, 1),
                new Vector2Int(1, 0),
                new Vector2Int(1, -1),
                new Vector2Int(0, -1),
                new Vector2Int(-1, 1),
                new Vector2Int(-1, 0),
                new Vector2Int(-1, 1),
            };


        public static ImageRect AddOutline(ImageRect rect)
        {
            var outlinedData = new List<Vector2Int>();
            outlinedData.AddRange(rect.Data);

            foreach (var point in rect.Data)
            {
                foreach (var vector in _addingVectors)
                {
                    var outlinedPoint = point + vector;

                    if (!outlinedData.Contains(outlinedPoint) && rect.CanContainPoint(outlinedPoint))
                        outlinedData.Add(outlinedPoint);
                }
            }

            rect.Data = outlinedData;
            return rect;
        }
    }
}