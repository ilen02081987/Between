using System.IO;
using UnityEngine;
using UnityEngine.AI;
using Between.Animations;
using Between.Damage;
using Between.Teams;
using Between.UI.Enemies;

namespace Between.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseEnemy : BaseDamagableObject
    {
        private static HealthBar _bar;
        public override Team Team => Team.Enemies;
        protected PlayerController player { get; private set; }

        [SerializeField] protected NpcAnimator animator;
        [SerializeField] protected DamageItem _damage;

        [SerializeField] private float _destroyTime = 2;
        [SerializeField] private Transform _healthBarAnchor;

        private Collider _collider;

        protected virtual void Start()
        {
            _collider = GetComponent<Collider>();
            animator.AttachTo(this);

            player = Player.Instance.Controller;
            player.OnDie += PerformOnPlayerDie;

            InitDamagableObject();
            CreateHealthBar();
        }

        protected abstract void PerformOnPlayerDie();

        protected override void PerformOnDie()
        {
            _collider.isTrigger = true;
            Destroy(gameObject, _destroyTime);
        }

        private void CreateHealthBar()
        {
            if (_bar == null)
            {
                string path = Path.Combine(ResourcesFoldersNames.UI, "NpcHealthBar");
                _bar = Resources.Load<HealthBar>(path);
            }

            HealthBar barInstance = Instantiate(_bar, LevelManager.Instance.Canvas.transform);
            barInstance.AttachTo(this, _healthBarAnchor);
        }
    }
}