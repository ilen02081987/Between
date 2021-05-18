using RH.Utilities.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviourSingleton<GameCamera>
{
    public static Camera MainCamera => Instance._mainCamera;

    [SerializeField] private Camera _mainCamera;

    public static Vector3 ScreenToWorldPoint(Vector3 screenPoint)
    {
        var zDistance = - MainCamera.transform.position.z;
        return MainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, zDistance));
    }
}
