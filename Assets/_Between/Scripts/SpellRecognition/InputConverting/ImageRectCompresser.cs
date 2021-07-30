using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellRecognition.InputConversion
{
    public class ImageRectCompresser
    {
        private static Vector2Int _compressedImageResolution = new Vector2Int(28, 28);

        public static ImageRect Compress(ImageRect rect)
        {
            var compressedData = new List<Vector2Int>();

            foreach (var value in rect.Data)
            {
                var compressedValue = new Vector2Int(
                    (value.x - rect.MinPoint.x) * _compressedImageResolution.x / rect.Width,
                    (value.y - rect.MinPoint.y) * _compressedImageResolution.y / rect.Height);

                if (!compressedData.Contains(compressedValue))
                    compressedData.Add(compressedValue);
            }

            var compressedRect = ImageRectCreator.CreateRect(compressedData);
            return compressedRect;
        }
    }
}