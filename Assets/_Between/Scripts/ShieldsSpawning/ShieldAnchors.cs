using System;
using UnityEngine;

namespace Between.ShieldsSpawning
{
    public partial class NpcShieldSpawner
    {
        [Serializable]
        private class ShieldAnchors
        {
            public Transform StartPoint;
            public Transform EndPoint;
        }
    }
}