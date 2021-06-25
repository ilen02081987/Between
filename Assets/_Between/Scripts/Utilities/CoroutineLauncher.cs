using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Utilities
{
    public class CoroutineLauncher : MonoBehaviourSingleton<CoroutineLauncher>
    {
        public static void Start(IEnumerator coroutine) => Instance.StartCoroutine(coroutine);
        public static void Stop(IEnumerator coroutine) => Instance.StopCoroutine(coroutine);
        public static void StopAll() => Instance.StopAllCoroutines();
    }
}