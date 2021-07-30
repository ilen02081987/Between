using System.Collections;
using UnityEngine;
using Between.SpellsEffects.ShieldSpell;
using Between.Utilities;

namespace Between.ShieldsSpawning
{
    public partial class NpcShieldSpawner : MonoBehaviour
    {
        [SerializeField] private ProjectileTrigger[] _triggers;
        [SerializeField] private NpcShieldController _controller;
        [SerializeField] private ShieldAnchors[] _shieldsAnchors;
        [SerializeField] private Transform _owner;

        private ShieldSpawner _shieldSpawner;

        private void Start()
        {
            _shieldSpawner = new ShieldSpawner("MavkaShield", _owner);

            foreach (ProjectileTrigger trigger in _triggers)
                trigger.OnDetect += TrySpawnShield;
        }

        private void OnDestroy()
        {
            foreach (ProjectileTrigger trigger in _triggers)
                trigger.OnDetect -= TrySpawnShield;
        }

        private void TrySpawnShield()
        {
            if (_controller.IsCooldown || _shieldsAnchors == null)
                return;

            foreach (ShieldAnchors anchors in _shieldsAnchors)
                _shieldSpawner.Spawn(anchors.StartPoint.position, anchors.EndPoint.position);

            CoroutineLauncher.Start(WaitCooldown(GameSettings.Instance.MavkaShieldsCooldownTime));
        }

        private IEnumerator WaitCooldown(float time)
        {
            _controller.IsCooldown = true;
            yield return new WaitForSeconds(time);
            _controller.IsCooldown = false;
        }
    }
}