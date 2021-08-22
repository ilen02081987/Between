using UnityEngine;

namespace Between.Vfx
{
    public class DelayedDestroyer : MonoBehaviour
    {
        [SerializeField] private float _time;

        private void Start() => Destroy(gameObject, _time);
    }
}