using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells.Shield
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 3f;

        private void Start()
        {
            StartCoroutine(WaitForDestroy());
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);

            if (this != null)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            StopCoroutine(WaitForDestroy());
        }
    }
}