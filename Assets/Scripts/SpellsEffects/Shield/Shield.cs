using Between.Interfaces;
using Between.Teams;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellsEffects.Shield
{
    public class Shield : MonoBehaviour, IDamagable
    {
        public float Size => transform.localScale.y;
        public Team Team { get; set; } = Team.Player;

        [SerializeField] private float _lifeTime = 3f;

        private void Start()
        {
            StartCoroutine(WaitToDestroy());
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);

            if (this != null && gameObject != null)
                DestroyShield();
        }

        public void ApplyDamage(float damage) => DestroyShield();

        private void DestroyShield()
        {
            StopCoroutine(WaitToDestroy());
            Destroy(gameObject);
        }
    }
}