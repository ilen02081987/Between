using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellRecognition.InputConversion
{
    public struct ImageRect
    {
        public Vector2Int MinPoint;
        public Vector2Int MaxPoint;

        public int Width => MaxPoint.x - MinPoint.x;
        public int Height => MaxPoint.y - MinPoint.y;

        public List<Vector2Int> Data;

        public ImageRect(Vector2Int minPoint, Vector2Int maxPoint, List<Vector2Int> data = null)
        {
            MinPoint = minPoint;
            MaxPoint = maxPoint;
            Data = data;
        }

        public bool CanContainPoint(Vector2Int point) => point.x <= Width && point.y <= Height &&
            point.x >= 0 && point.y >= 0;
    }
}