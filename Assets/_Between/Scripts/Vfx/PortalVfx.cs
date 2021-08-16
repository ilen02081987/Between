using System.Collections;
using UnityEngine;

namespace Between.Vfx
{
    public class PortalVfx : MonoBehaviour
    {
        public GameObject portalOpen;
        public GameObject portalIdle;
        public GameObject portalClose;

        public float portalLifetime = 2.5f;

        private void Start()
        {
            OpenPortal();
        }

        public void OpenPortal()
        {
            StartCoroutine("PortalLoop");
        }

        IEnumerator PortalLoop()
        {
            GameObject portalOpener = (GameObject)Instantiate(portalOpen, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.8f);
            GameObject portalIdler = (GameObject)Instantiate(portalIdle, transform.position, transform.rotation);

            yield return new WaitForSeconds(portalLifetime);
            Destroy(portalIdler);
            Destroy(portalOpener);
            GameObject portalCloser = (GameObject)Instantiate(portalClose, transform.position, transform.rotation);

            yield return new WaitForSeconds(1f);
            Destroy(portalCloser);

            Destroy(gameObject);
        }
    }
}