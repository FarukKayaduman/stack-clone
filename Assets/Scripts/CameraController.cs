using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController m_Instance = null;
    public static CameraController Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (CameraController)FindObjectOfType(typeof(CameraController));
            }
            return m_Instance;
        }
    }

    Transform cameraTransform;
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }
        
    public void UpdateCameraPosition()
    {
        int lastChildIndex = PieceManager.Instance.transform.childCount - 1;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position,
            new Vector3(cameraTransform.position.x, PieceManager.Instance.transform.GetChild(lastChildIndex).position.y + 14.75f, cameraTransform.position.z),
            2.0f * Time.deltaTime);
    }
}
