using System;
using UnityEngine;

namespace Between
{
    public class CameraViewTrigger : MonoBehaviour
    {
        public event Action Destroyed;

        public Vector3 ChangeTo;
        public float ChangeTime;
        public float RestoreTime;

        private void OnDestroy() => Destroyed?.Invoke();
    }
}