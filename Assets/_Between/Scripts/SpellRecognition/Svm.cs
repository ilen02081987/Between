using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Accord.IO;
using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;

namespace Between.SpellRecognition
{
    public class Svm
    {
        private static string _coefficientsPath => Path.Combine(Application.streamingAssetsPath, "SvmCoefficients.svm");

        private MulticlassSupportVectorMachine<Linear> svm;

        private float[] _decideBorders = GameSettings.Instance.DecideBorder;

        public Svm()
        {
            if (File.Exists(_coefficientsPath))
                svm = Serializer.Load<MulticlassSupportVectorMachine<Linear>>(_coefficientsPath);
            else
                throw new Exception($"There is no SVM coefficients files at {_coefficientsPath}");
        }

        public SpellFigure Recognize(List<Vector2Int> input)
        {
            double[] svmData = InputToSvmDataConverter.Convert(input);

            if (svmData == null)
                return SpellFigure.None;

            double[] probabilities = svm.Probabilities(svmData);

            LogProbabilities(probabilities);

            for (int i = 0; i < probabilities.Length; i++)
            {
                if (probabilities[i] > _decideBorders[i])
                    return (SpellFigure)i;
            }

            return SpellFigure.None;
        }

        private void LogProbabilities(double[] probabilities)
        {
            if (!GameSettings.Instance.EnableProbabilitiesLog)
                return;

            string message = "";

            for (int i = 0; i < probabilities.Length; i++)
                message += $"{probabilities[i].ToString("F2")} ";

            Debug.Log(message);
        }
    }
}