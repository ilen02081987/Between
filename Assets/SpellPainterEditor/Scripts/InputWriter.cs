using Between;
using Between.InputTracking;
using System.IO;
using UnityEngine;

namespace SpellPainterEditor
{
    public class InputWriter : MonoBehaviour
    {
        [SerializeField] private string _fileName;

        private SpellInputArray _inputArray;

        private void Start()
        {
            _inputArray = new SpellInputArray();

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
            _inputArray.AddInput(
                new SpellInput(SpellInputType.Dot, GameCamera.ScreenToWorldPoint(input.Position)));
        }
        
        private void AddSpace(InputData input)
        {
            Debug.Log("Add space");
            _inputArray.AddInput(new SpellInput(SpellInputType.Space, Vector3.zero));
        }
        
        private void SaveInput()
        {
            Debug.Log("Save");

            _inputArray.ConvertDirtyData();

            string path = Path.Combine(Application.streamingAssetsPath, _fileName + ".json");
            string convertedData = JsonUtility.ToJson(_inputArray);

            File.WriteAllText(path, convertedData);
        }
    }
}