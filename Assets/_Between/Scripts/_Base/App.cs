using Between.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Between
{
    public class App : MonoBehaviour
    {
        private void Awake()
        {
            new DataManager();
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}