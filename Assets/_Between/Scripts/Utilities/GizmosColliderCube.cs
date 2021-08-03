using UnityEngine;

namespace Between.Utilities
{
    [RequireComponent(typeof(BoxCollider))]
    public class GizmosColliderCube : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _boxCollider.size);
        }
    }
}