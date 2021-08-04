using System.Collections.Generic;
using UnityEngine;
using Between.Data;
using Between.Saving;
using Between.Spawning;

namespace Between.LevelObjects
{
    public class CheckPoint : InteractableObject
    {
        [SerializeField] private SpawnPoint _playerSpawnPoint;

        private int _number;

        public void AttachNumber(int number) => _number = number;

        protected override void PerformOnInteract()
        {
            Player.Instance.Controller.Heal(Mathf.Infinity);
            Player.Instance.Mana.Add(Mathf.Infinity);

            SaveSystem.Save(new PlayerData(gameObject.scene.buildIndex, _number, CollectExistingGameObjects()));
        }

        public Player LoadPlayer()
        {
            var player = _playerSpawnPoint.Spawn();
            return new Player(player.GetComponent<PlayerController>());
        }
        
        private List<int> CollectExistingGameObjects()
        {
            var existingObjects = new List<int>();
            var savingObjects = FindObjectsOfType<SavableObject>();

            foreach (var savingObject in savingObjects)
                existingObjects.Add(savingObject.Id);

            return existingObjects;
        }
    }
}