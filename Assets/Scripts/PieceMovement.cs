using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    float blockSpeed = 7.0f;
    short direction = -1;
    float positionLimit = 7.0f;

    public static PieceMovement instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

        if (PieceManager.Instance.movingAxisOfTheBlock == PieceManager.Axes.x)
        {
            MovePieceXAxis();
        }
        else
        {
            MovePieceZAxis();
        }
    }

    void MovePieceXAxis()
    {
        float xPosition = transform.position.x + blockSpeed * Time.deltaTime * direction; // Change the position of the block on the X Axis
        Mathf.Clamp(xPosition, -positionLimit, positionLimit); // Limit X position of the block
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z); // Assign new position of the block
        if (transform.position.x <= -positionLimit || transform.position.x >= positionLimit) // If position of the block reaches to limits, reverse the direction
        {
            direction *= -1; 
        }
    }

    void MovePieceZAxis()
    {
        float zPosition = transform.position.z + blockSpeed * Time.deltaTime * direction; // Change the position of the block on the Z Axis
        Mathf.Clamp(zPosition, -positionLimit, positionLimit); // Limit Z position of the block
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition); // Assign new position of the block
        if (transform.position.z <= -positionLimit || transform.position.z >= positionLimit) // If position of the block reaches to limits, reverse the direction
        {
            direction *= -1;
        }
    }

    public void StopPiece()
    {
        blockSpeed = 0;
    }
}
