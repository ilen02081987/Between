using Between.Spawning;
using System;
using UnityEngine;

namespace Between.LevelObjects
{
    public class CheckPoint : InteractableObject
    {
        public event Action<CheckPoint> OnInteract;

        public override void Interact()
        {
            Player.Instance.Controller.Heal(Mathf.Infinity);
            Player.Instance.Mana.Add(Mathf.Infinity);

            OnInteract?.Invoke(this);
        }
    }
}