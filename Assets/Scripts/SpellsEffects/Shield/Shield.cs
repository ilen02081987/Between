using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.SpellsEffects.Shield
{
    public class Shield : MonoBehaviour
    {
        public float Size => transform.localScale.y;

        [SerializeField] private float _lifeTime = 3f;

        private void Start()
        {
            StartCoroutine(WaitToDestroy());
        }

        private void OnDestroy()
        {
            StopCoroutine(WaitToDestroy());
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);

            if (this != null)
                Destroy(gameObject);
        }
    }
}