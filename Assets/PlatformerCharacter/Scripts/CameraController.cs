using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Большой шаг приближения/отдаления камеры"), SerializeField] private float zoomBig;
    [Tooltip("Маленький шаг приближения/отдаления камеры"), SerializeField] private float zoomLittle;

    private Camera cm;

    private void Start()
    {
        cm = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {

        if (cm.fieldOfView > 179) {
            cm.fieldOfView = 179;
        }

        if (cm.fieldOfView < 1)
        {
            cm.fieldOfView = 1;
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            cm.fieldOfView += zoomBig;
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            cm.fieldOfView -= zoomBig;
        }

        if (Input.GetKeyDown(KeyCode.End))
        {
            cm.fieldOfView += zoomLittle;
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            cm.fieldOfView -= zoomLittle;
        }

    }

}
