using System.Collections.Generic;
using UnityEngine;
using Between.Enemies.Mavka;
using Between.SpellsEffects.Projectile;
using Between.Teams;
using Between;
using System.Collections;
using Between.Utilities;
using Between.SpellsEffects.ShieldSpell;
using UnityEngine.UIElements;
using System;

public class ProjectileTrigger : MonoBehaviour
{
    private static bool _isCooldown = false;

    private static ShieldSpawner _shieldSpawner;

    [SerializeField] private ShieldAnchors[] _shieldsAnchors;

    private List<ProjectileTragectoryData> _projectileTragectoryDatas = new List<ProjectileTragectoryData>();


    private void Start()
    {
        if (_shieldSpawner == null)
            _shieldSpawner = new ShieldSpawner("MavkaShield");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out var projectile))
            if (projectile.Team != Team.Enemies)
                _projectileTragectoryDatas.Add(new ProjectileTragectoryData(projectile, other.transform.position));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out var projectile))
        {
            var projectileData = FindData(projectile);

            if (projectileData == null)
                return;
            
            projectileData.AddExitPoint(other.transform.position);

            if (projectileData.CanHitMavka())
                TrySpawnShield(_shieldsAnchors);
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

    private class ProjectileTragectoryData
    {
        public Projectile Projectile;
        public Vector3 EnterPoint;
        public Vector3 ExitPoint;

        public ProjectileTragectoryData(Projectile projectile, Vector3 enterPoint)
        {
            Projectile = projectile;
            EnterPoint = enterPoint;
        }

        public void AddExitPoint(Vector3 point)
        {
            ExitPoint = point;
        }

        public bool CanHitMavka()
        {
            if (Physics.Raycast(ExitPoint, (ExitPoint - EnterPoint).normalized, out RaycastHit info, 100f))
                return info.collider.TryGetComponent<MavkaController>(out var mavka);

            return false;
        }
    }

    [Serializable]
    private class ShieldAnchors
    {
        public Transform StartPoint;
        public Transform EndPoint;
    }
}
