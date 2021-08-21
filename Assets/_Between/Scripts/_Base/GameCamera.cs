using Between.Utilities;
using UnityEngine;

namespace Between
{
    [RequireComponent(typeof(Camera))]
    public class GameCamera : MonoBehaviourSingleton<GameCamera>
    {
        public static Camera MainCamera
        {
            get
            {
                if (Instance._mainCamera == null)
                    Instance._mainCamera = Instance.GetComponent<Camera>();

                return Instance._mainCamera;
            }
        }

        private Camera _mainCamera;

        public static Vector3 ScreenToWorldPoint(Vector2Int screenPoint)
        {
            var zDistance = - MainCamera.transform.position.z;
            return MainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, zDistance));
        }

        public static Vector3 WorldToScreenPoint(Vector3 from) => MainCamera.WorldToScreenPoint(from);
    }
}