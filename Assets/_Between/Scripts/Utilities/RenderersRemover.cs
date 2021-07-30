using UnityEngine;

namespace Between.Utilities
{
    public class RenderersRemover : MonoBehaviour
    {
        private void Awake()
        {
<<<<<<< HEAD
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<MeshFilter>());
=======
#if !UNITY_EDITOR
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<MeshFilter>());
#endif
>>>>>>> Prototype_1.0
        }
    }
}