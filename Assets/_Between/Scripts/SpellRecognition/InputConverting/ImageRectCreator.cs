using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellRecognition.InputConversion
{
    public class ImageRectCreator
    {
        public static ImageRect CreateRect(List<Vector2Int> input)
        {
            int minX = Screen.width;
            int minY = Screen.height;

            int maxX = 0;
            int maxY = 0;

            foreach (var point in input)
            {
                if (point.x < minX)
                    minX = point.x;

                if (point.y < minY)
                    minY = point.y;

                if (point.x > maxX)
                    maxX = point.x;

                if (point.y > maxY)
                    maxY = point.y;
            }

            return new ImageRect(new Vector2Int(minX, minY), new Vector2Int(maxX, maxY), input);
        }
    }
}