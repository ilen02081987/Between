using System.Collections.Generic;
using UnityEngine;
using Between.SpellRecognition.InputConversion;

namespace Between.SpellRecognition
{
    public class InputToSvmDataConverter
    {
        public static double[] Convert(List<Vector2Int> inputPoints)
        {
            ImageRect rect = ImageRectCreator.CreateRect(inputPoints);

            if (rect.Width == 0 || rect.Height == 0)
                return null;

            ImageRect compressedRect = ImageRectCompresser.Compress(rect);
            ImageRect outlinedRect = InputOutliner.AddOutline(compressedRect);
            List<double> svmData = SVMDataConverter.ToSVMData(outlinedRect);

            return svmData.ToArray();
        }
    }
}