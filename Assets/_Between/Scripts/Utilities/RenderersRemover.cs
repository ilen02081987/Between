using UnityEngine;

namespace Between.Utilities
{
    public class RenderersRemover : MonoBehaviour
    {
        private void Awake()
        {
#if !UNITY_EDITOR
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<MeshFilter>());
#endif
        }
    }
}