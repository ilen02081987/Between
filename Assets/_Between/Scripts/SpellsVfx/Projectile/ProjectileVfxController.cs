using UnityEngine;
using System.Collections.Generic;

namespace Between.SpellsEffects.Projectile
{
    public class ProjectileVfxController : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

        [Range(0,100), SerializeField] private float accuracy;
        [SerializeField] private GameObject muzzlePrefab;
        [SerializeField] private GameObject hitPrefab;
        [SerializeField] private List<GameObject> trails;

        private Vector3 offset;
        private bool collided;

        private void Start()
        {
            UpdateOffset();
            CreateMuzzle();

            _projectile.OnHit += PerformOnHit;
        }

        private void UpdateOffset()
        {
            if (accuracy != 100)
            {
                accuracy = 1 - (accuracy / 100);

                for (int i = 0; i < 2; i++)
                {
                    var val = 1 * Random.Range(-accuracy, accuracy);
                    var index = Random.Range(0, 2);

                    if (i == 0)
                    {
                        if (index == 0)
                            offset = new Vector3(0, -val, 0);
                        else
                            offset = new Vector3(0, val, 0);
                    }
                    else
                    {
                        if (index == 0)
                            offset = new Vector3(0, offset.y, -val);
                        else
                            offset = new Vector3(0, offset.y, val);
                    }
                }
            }
        }

        private void CreateMuzzle()
        {
            if (muzzlePrefab != null)
            {
                var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
                var ps = muzzleVFX.GetComponent<ParticleSystem>();

                muzzleVFX.transform.forward = gameObject.transform.forward + offset;

                if (ps != null)
                    Destroy(muzzleVFX, ps.main.duration);
                else
                {
                    var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(muzzleVFX, psChild.main.duration);
                }
            }
        }

        private void PerformOnHit(Collider collider)
        {
            _projectile.OnHit -= PerformOnHit;

            if (!collided)
            {
                collided = true;

                if (trails.Count > 0)
                {
                    for (int i = 0; i < trails.Count; i++)
                    {
                        if (trails[i] == null)
                            continue;

                        trails[i].transform.parent = null;

                        if (trails[i].TryGetComponent<ParticleSystem>(out var ps))
                        {
                            ps.Stop();
                            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                        }
                    }
                }

                Vector3 contactPoint = FindContactPoint(collider);

                if (hitPrefab != null)
                {
                    var hitVFX = Instantiate(hitPrefab, contactPoint, Quaternion.identity) as GameObject;

                    var ps = hitVFX.GetComponent<ParticleSystem>();
                    if (ps == null)
                    {
                        var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitVFX, psChild.main.duration);
                    }
                    else
                    {
                        Destroy(hitVFX, ps.main.duration);
                    }
                }

                Destroy(gameObject);
            }
        }

        private Vector3 FindContactPoint(Collider collider)
        {
            MeshCollider meshCollider = collider as MeshCollider;

            if (meshCollider != null && !meshCollider.convex)
                return transform.position;
            else
                return collider.ClosestPoint(transform.position);
        }
    }
}