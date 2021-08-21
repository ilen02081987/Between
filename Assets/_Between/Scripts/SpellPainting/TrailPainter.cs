using UnityEngine;

namespace Between.SpellPainting
{
    public class TrailPainter : BasePainter
    {
        [SerializeField] private GameObject _trailPrefab;

        private GameObject _currentTrail;

        public override void Draw(Vector3 point)
        {
            if (_currentTrail == null)
                CreateNewTrail();

            _currentTrail.transform.position = _startPoint + point;
        }

        public override void AddSpace()
        {
            _currentTrail = null;
        }

        private void CreateNewTrail() => _currentTrail = Instantiate(_trailPrefab, transform);
    }
}