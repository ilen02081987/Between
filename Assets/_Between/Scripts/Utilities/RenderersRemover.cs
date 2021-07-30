using UnityEngine;

namespace Between.Utilities
{
    public class RenderersRemover : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<MeshFilter>());
        }
    }
}