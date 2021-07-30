using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.LevelObjects
{
    public class ManaBottle : InteractableObject
    {
        public override void Interact()
        {
            Player.Instance.ManaBottlesHolder.Add();
            Destroy();
        }

        public override void DestroyOnLoad() => Destroy();
    }
}