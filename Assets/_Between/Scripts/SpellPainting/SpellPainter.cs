using System;
using System.Collections;
using System.IO;
using UnityEngine;
using SpellPainterEditor;
using RH.Utilities.Coroutines;

namespace Between.SpellPainting
{
    public class SpellPainter
    {
        public event Action Complete;

        private readonly float _drawTime;
        private SpellInputArray _inputArray;

        private IPainter _painter;
        private WaitForSeconds _afterDrawDelay;

        public SpellPainter(string spellName, string painterName, Vector3 startPoint
            , float drawTime, float afterDrawDelay)
        {
            LoadInputData(spellName);
            CreatePainter(painterName, startPoint);

            _drawTime = drawTime;
            _afterDrawDelay = new WaitForSeconds(afterDrawDelay);
        }

        private void LoadInputData(string spellName)
        {
            string spellPath = Path.Combine(Application.streamingAssetsPath, spellName + ".json");
            _inputArray = JsonUtility.FromJson<SpellInputArray>(File.ReadAllText(spellPath));
            _inputArray.Compress(GameSettings.Instance.SpellPictureCompressCoefficient);
        }

        private void CreatePainter(string painterName, Vector3 startPoint)
        {
            _painter = MonoBehaviour.Instantiate(Resources.Load<TrailPainter>
                (Path.Combine(ResourcesFoldersNames.SPELL_PAINTERS, painterName)));

            _painter.Init(startPoint);
        }

        public void StartDraw()
        {
            CoroutineLauncher.Start(Draw());
        }

        private IEnumerator Draw()
        {
            WaitForSeconds pointDelay = new WaitForSeconds(_drawTime / _inputArray.Inputs.Count);

            for (int i = 0; i < _inputArray.Inputs.Count; i++)
            {
                if (_inputArray.Inputs[i].Type == SpellInputType.Dot)
                    _painter.Draw(_inputArray.Inputs[i].Point);
                else
                    _painter.AddSpace();

                yield return pointDelay;
            }

            yield return _afterDrawDelay;

            CompleteDraw();
        }

        private void CompleteDraw()
        {
            Complete?.Invoke();
            _painter.Destroy();
        }
    }
}