using RH.Utilities.SingletonAccess;
using System.Collections;
using UnityEngine;

namespace Between.Utilities
{
    public class CoroutineLauncher : MonoBehaviourSingleton<CoroutineLauncher>
    {
        public static Coroutine Start(IEnumerator coroutine) => Instance.StartCoroutine(coroutine);
        public static void Stop(IEnumerator coroutine) => Instance.StopCoroutine(coroutine);
        public static void StopAll() => Instance.StopAllCoroutines();
    }
}