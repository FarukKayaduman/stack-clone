using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        UpdateCameraPosition();
    }

    // Updates position of the Main Camera
    public void UpdateCameraPosition()
    {
        int lastChildIndex = PieceManager.Instance.transform.childCount - 1; // Get index of the last child object of PieceManager

        // Track the last instantiated piece smoothly
        transform.position = Vector3.Lerp(
                        transform.position,
            new Vector3(transform.position.x, PieceManager.Instance.transform.GetChild(lastChildIndex).position.y + 14.75f, transform.position.z), // 14.75f is Main Camera offset on Y axis
                        2.0f * Time.deltaTime); // Camera moving speed
    }
}
