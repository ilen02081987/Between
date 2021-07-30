using UnityEngine;
using UnityEngine.SceneManagement;
using Between.Data;
using Between.Saving;
using Between.Spawning;
using Between.Enemies;

namespace Between.LevelObjects
{
    public class CheckPoint : InteractableObject
    {
        [SerializeField] private SpawnPoint _playerSpawnPoint;
        [SerializeField] private GameObject _zone;

        private int _number;

        public void AttachNumber(int number) => _number = number;

        public override void Interact()
        {
            Player.Instance.Controller.Heal(Mathf.Infinity);
            Player.Instance.Mana.Add(Mathf.Infinity);

            SaveSystem.Save(new PlayerData(gameObject.scene.buildIndex, _number));
        }

        public void Clear()
        {
            Destroy(_zone);
        }

        public Player LoadPlayer()
        {
            var player = _playerSpawnPoint.Spawn();
            return new Player(player.GetComponent<PlayerController>());
        }
    }
}