using UnityEngine;

namespace Between.Vfx
{
    public class ManaBottleUsingVfx : MonoBehaviour
    {
        [SerializeField] private GameObject _vfxPrefab;

        private void Start()
        {
            Player.Instance.ManaBottlesUser.BottleUsed += EnableVfx;
        }

        private void OnDestroy()
        {
            if (Player.Instance != null)
                Player.Instance.ManaBottlesUser.BottleUsed -= EnableVfx;
        }

        private void EnableVfx()
        {
            Instantiate(_vfxPrefab, transform);
        }
    }
}