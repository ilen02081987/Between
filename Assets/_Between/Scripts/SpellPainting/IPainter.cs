using UnityEngine;

namespace Between.SpellPainting
{
    public interface IPainter
    {
        public void Init(Vector3 point);
        public void Draw(Vector3 point);
        public void AddSpace();
    }
}