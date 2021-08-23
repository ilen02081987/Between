using System;
using UnityEngine;

namespace Between.Hacks
{
    public partial class HacksHandler
    {
        public class Hack
        {
            public readonly KeyCode Key;
            private readonly Action Action;

            public Hack(KeyCode key, Action action)
            {
                Key = key;
                Action = action;
            }

            public void Execute() => Action();
        }
    }
}