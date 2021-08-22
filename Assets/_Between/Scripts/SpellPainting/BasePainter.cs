using UnityEngine;

namespace Between.SpellPainting
{
    public abstract class BasePainter : MonoBehaviour, IPainter
    {
        protected Vector3 _startPoint;

        public void Init(Vector3 point) => _startPoint = point;

        public abstract void AddSpace();
        public abstract void Draw(Vector3 point);

        public void Destroy() => Destroy(gameObject);
    }
}