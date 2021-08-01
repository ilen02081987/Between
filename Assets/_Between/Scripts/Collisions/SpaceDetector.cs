using UnityEngine;

namespace Between.Collisions
{
    public class SpaceDetector
    {
        public static bool IsFreeSpace(Vector3 position, float radius)
        {
            Collider[] colliders = Physics.OverlapSphere(position, radius);

            foreach (var collider in colliders)
                if (!collider.CompareTag("Not colliding"))
                    return false;

            return true;
        }

        public static bool IsGrounded(Vector3 position)
        {
            Collider[] colliders = Physics.OverlapSphere(position, 1.4f);

            foreach (var collider in colliders)
                if (collider.CompareTag("Ground"))
                    return true;

            return false;
        }
    }
}