using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [Tooltip("������� ��� �����������/��������� ������"), SerializeField] private float zoomBig;
    [Tooltip("��������� ��� �����������/��������� ������"), SerializeField] private float zoomLittle;

    private CinemachineVirtualCamera cm;

    private void Start()
    {
        cm = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {

        //if (cm.m_Lens.OrthographicSize > 179) {
        //    cm.m_Lens.FieldOfView = 179;
        //}

        //if (cm.m_Lens.FieldOfView < 1)
        //{
        //    cm.m_Lens.FieldOfView = 1;
        //}

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            cm.m_Lens.OrthographicSize += zoomBig;
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            cm.m_Lens.OrthographicSize -= zoomBig;
        }

        if (Input.GetKeyDown(KeyCode.End))
        {
            cm.m_Lens.OrthographicSize += zoomLittle;
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            cm.m_Lens.OrthographicSize -= zoomLittle;
        }

    }

}
