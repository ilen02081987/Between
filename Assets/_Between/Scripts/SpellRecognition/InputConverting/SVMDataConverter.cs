using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellRecognition.InputConversion
{
    public class SVMDataConverter
    {
        public static List<double> ToSVMData(ImageRect rect)
        {
            var svmData = new List<double>();

            for (int i = 0; i < rect.Width; i++)
                for (int j = 0; j < rect.Height; j++)
                    svmData.Add(rect.Data.Contains(new Vector2Int(i, j)) ? 1 : 0);

            return svmData;
        }
    }
}