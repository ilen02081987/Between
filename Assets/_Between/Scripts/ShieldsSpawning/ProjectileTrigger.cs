using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.Teams;
using Between.Utilities;
using Between.SpellsEffects.ShieldSpell;

namespace Between.ShieldsSpawning
{
    public partial class ProjectileTrigger : MonoBehaviour
    {
        private static bool _isCooldown = false;

        private static ShieldSpawner _shieldSpawner;

        [SerializeField] private ShieldAnchors[] _shieldsAnchors;
        [SerializeField] private Transform _owner;

        private List<ProjectileTragectoryData> _projectileTragectoryDatas = new List<ProjectileTragectoryData>();

        private void Start()
        {
            if (_shieldSpawner == null)
                _shieldSpawner = new ShieldSpawner("MavkaShield", _owner);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Projectile>(out var projectile))
                if (projectile.Team != Team.Enemies)
                    _projectileTragectoryDatas.Add(
                        new ProjectileTragectoryData(projectile, other.transform.position, _owner.gameObject));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Projectile>(out var projectile))
            {
                var projectileData = FindData(projectile);

                if (projectileData == null)
                    return;

                projectileData.AddExitPoint(other.transform.position);

                if (projectileData.CanHitTarget())
                    TrySpawnShield(_shieldsAnchors);

                _projectileTragectoryDatas.Remove(projectileData);
            }
        }

        private static void TrySpawnShield(ShieldAnchors[] anchorsArray)
        {
            if (_isCooldown || anchorsArray == null)
                return;

            foreach (ShieldAnchors anchors in anchorsArray)
                _shieldSpawner.Spawn(anchors.StartPoint.position, anchors.EndPoint.position);

            CoroutineLauncher.Start(WaitCooldown(GameSettings.Instance.MavkaShieldsCooldownTime));
        }

        private static IEnumerator WaitCooldown(float time)
        {
            _isCooldown = true;
            yield return new WaitForSeconds(time);
            _isCooldown = false;
        }

        private ProjectileTragectoryData FindData(Projectile projectile)
            => _projectileTragectoryDatas.Find(x => x.Projectile == projectile);
    }
}