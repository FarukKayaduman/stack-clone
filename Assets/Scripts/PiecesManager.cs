using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : MonoBehaviour
{
    [SerializeField] float blockSpeed = 7.0f;
    short reverseTheDirection = -1;
    float positionLimit = 6.0f;
    char movingAxisOfTheBlock = 'X'; // X refers to X axis, Z refers to Z axis

    void Start()
    {
        transform.position = new Vector3(7, 1.25f, 0); // Starting position of the first block
    }

    void Update()
    {
        MovePieceXAxis();
    }

    void MovePieceXAxis()
    {
        Debug.Log("X position: " + transform.position.x);
        float xPosition = transform.position.x + blockSpeed * Time.deltaTime * reverseTheDirection; // Change the position of the block on the X Axis
        Mathf.Clamp(xPosition, -positionLimit, positionLimit); // Limit X position of the block
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z); // Assign new position of the block
        if (transform.position.x <= -positionLimit || transform.position.x >= positionLimit) // If position of the block reaches to limits, reverse the direction
        {
            reverseTheDirection *= -1; 
        }
    }

    void MovePieceZAxis()
    {
        Debug.Log("Z position: " + transform.position.z);
        float zPosition = transform.position.z + blockSpeed * Time.deltaTime * reverseTheDirection; // Change the position of the block on the Z Axis
        Mathf.Clamp(zPosition, -positionLimit, positionLimit); // Limit Z position of the block
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition); // Assign new position of the block
        if (transform.position.z <= -positionLimit || transform.position.z >= positionLimit) // If position of the block reaches to limits, reverse the direction
        {
            reverseTheDirection *= -1;
        }
    }
}
