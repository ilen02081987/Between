using UnityEngine;

namespace Between.InputPainting
{
    public class InputPainterEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void ChangeTrailColor(Color to)
        {
            var trails = _particleSystem.trails;
            trails.colorOverLifetime = new ParticleSystem.MinMaxGradient(to);
        }
    }
}