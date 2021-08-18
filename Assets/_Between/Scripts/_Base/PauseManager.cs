using System;
using UnityEngine;

namespace Between
{
    public class PauseManager : MonoBehaviour
    {
        public static event Action OnPause;
        public static event Action OnPlay;

        public static bool IsPause { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ChangePauseState();
        }

        private void ChangePauseState()
        {
            if (IsPause)
                Play();
            else
                Pause();
        }

        public static void Play()
        {
            Time.timeScale = 1f;
            OnPlay?.Invoke();
        }

        public static void Pause()
        {
            Time.timeScale = 0f;
            OnPause?.Invoke();
        }
    }
}