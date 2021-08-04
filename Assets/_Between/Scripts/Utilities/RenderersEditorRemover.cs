using UnityEngine;

namespace Between.Utilities
{
    public class RenderersEditorRemover : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<MeshFilter>());
        }
    }
}