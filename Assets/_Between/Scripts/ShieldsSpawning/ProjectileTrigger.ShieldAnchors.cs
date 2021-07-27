using System;
using UnityEngine;

namespace Between.ShieldsSpawning
{
    public partial class ProjectileTrigger
    {
        [Serializable]
        private class ShieldAnchors
        {
            public Transform StartPoint;
            public Transform EndPoint;
        }
    }
}