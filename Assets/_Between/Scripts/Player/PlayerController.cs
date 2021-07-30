using UnityEngine;
using Between.Teams;
using Between.MainCharacter;
using UnityEngine.SceneManagement;
using System.Collections;
using Between.Utilities;

namespace Between
{ 
    public class PlayerController : BaseDamagableObject
    {
        public override Team Team => Team.Player;

        private LocomotionController _locomotionController;

        private void Start()
        {
            InitDamagableObject();
            _locomotionController = GetComponent<LocomotionController>();
        }

        public void Push(Vector3 direction, float force)
        {
            if (Mathf.Approximately(force, 0f))
                return;

            _locomotionController.Push(direction, force);
        }

        protected override void PerformOnDie()
        {
            CoroutineLauncher.Start(ReloadScene());
        }

        private IEnumerator ReloadScene()
        {
            Destroy(gameObject);

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}