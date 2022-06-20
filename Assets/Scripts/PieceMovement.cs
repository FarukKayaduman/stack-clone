using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    [SerializeField] private float pieceSpeed = 7.0f;
    private short direction = -1;
    public float positionLimit = 6.0f; // TODO: Adjust position limit each time a piece instantiated. May use "positionLimit -= fallingPieceScale / 2"

    // Create an instance of the object
    public static PieceMovement Instance;

    // Start is called before the first frame Update
    private void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        // Call mover methods depends on current moving axis
        if (PieceManager.Instance.Direction == PieceManager.Axes.x)
        {
            MovePieceXAxis();
        }
        else
        {
            MovePieceZAxis();
        }
    }

    // Move piece on X axis
    private void MovePieceXAxis()
    {
        // Change the position of the block on the X Axis and limit X position of the block
        float xPosition = Mathf.Clamp(transform.position.x + pieceSpeed * Time.deltaTime * direction, -positionLimit, positionLimit);
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z); // Assign new position of the block
        if (transform.position.x == -positionLimit || transform.position.x == positionLimit) // If position of the block reaches to limits, reverse the direction
        {
            direction *= -1; 
        }
    }

    // Move piece on Z axis
    private void MovePieceZAxis()
    {
        // Change the position of the block on the Z Axis and limit Z position of the block
        float zPosition = Mathf.Clamp(transform.position.z + pieceSpeed * Time.deltaTime * direction, -positionLimit, positionLimit);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition); // Assign new position of the block
        if (transform.position.z == -positionLimit || transform.position.z == positionLimit) // If position of the block reaches to limits, reverse the direction
        {
            direction *= -1;
        }
    }

    // Stops the piece by making pieceSpeed value 0
    public void StopPiece()
    {
        pieceSpeed = 0; 
    }
}
