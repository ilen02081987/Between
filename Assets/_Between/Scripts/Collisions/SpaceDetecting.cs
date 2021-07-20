using UnityEngine;

namespace Between.Collisions
{
    public class SpaceDetecting
    {
        public static bool IsFreeSpace(Vector3 position, float radius)
        {
            Collider[] colliders = Physics.OverlapSphere(position, radius);
            return colliders == null || colliders.Length == 0;
        }
    }
}