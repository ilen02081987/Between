using Between.UserInput;
using System.IO;
using UnityEngine;

namespace SpellPainter
{
    public class InputWriter : MonoBehaviour
    {
        [SerializeField] private string _fileName;
        private string _path;

        private SpellInputArray _inputArray;

        private void Start()
        {
            InputHandler.DrawCall += CollectInput;
            InputHandler.EndDraw += AddSpace;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                SaveInput();
        }

        private void CollectInput(InputData input)
        {
            _inputArray.AddInput(new SpellInput(SpellInputType.Dot, input.Position));
        }
        
        private void AddSpace(InputData input)
        {
            _inputArray.AddInput(new SpellInput(SpellInputType.Space, Vector2Int.zero));
        }
        
        private void SaveInput()
        {
            _inputArray.ConvertDirtyData();

            _path = Path.Combine(Application.streamingAssetsPath, _fileName);
            string convertedData = JsonUtility.ToJson(_inputArray);

            File.WriteAllText(_path, convertedData);
        }
    }
}