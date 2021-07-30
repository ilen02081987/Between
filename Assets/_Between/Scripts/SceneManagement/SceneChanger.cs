using UnityEngine.SceneManagement;

namespace Between.SceneManagement
{
    public static class SceneChanger
    {
        public static void ChangeScene(int from, int to)
        {
            if (from > 0)
                SceneManager.UnloadSceneAsync(from);

            SceneManager.LoadScene(to, LoadSceneMode.Additive);
        }
    }
}