using UnityEngine;
using Between.LevelObjects;

namespace Between.Vfx
{
    public class FightBordersVfx : MonoBehaviour
    {
        [SerializeField] private FightBorders _borders;
        [SerializeField] private GameObject[] _effects;

        private void Start()
        {
            _borders.OnActivate += EnableEffects;
        }

        private void OnDestroy()
        {
            _borders.OnActivate -= EnableEffects;
        }

        private void EnableEffects()
        {
            foreach (var effect in _effects)
                effect.SetActive(true);
        }
    }
}