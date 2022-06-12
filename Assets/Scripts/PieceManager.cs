using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject fallingPiecePrefab;
    [SerializeField] private GameObject prevPiece, newPiece, fallingPiece;

    [SerializeField] private Transform fallingPieceContainerTransform;

    private float towerHeight = 0; // Tower height is 0 at start

    public bool isGameEnded = false;

    private Vector3 fallingPieceScale, fallingPiecePosition, newPieceScale, newPiecePosition;

    public enum Axes {x, z}; // x refers to X axis, z refers to Z axis
    public Axes Direction = Axes.x; // Game always starts by piece moving on X-axis

    // Create an instance of the object
    public static PieceManager m_Instance = null;
    //Singleton
    public static PieceManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (PieceManager)FindObjectOfType(typeof(PieceManager));
            }
            return m_Instance;
        }
    }
    // Start is called before the first frame Update
    private void Start()
    {
        InitializeGameAtStart();
    }

    // Update is called once per frame
    private void Update()
    {
        SetTowerHeight();


        if (Input.GetMouseButtonDown(0)) // Left click
        {
            isGameEnded = IsOutOfPrevPiece();

            if (isGameEnded)
            {
                OnGameOver(); // TODO
            }
            else
            {
                PieceMovement.Instance.StopPiece(); // Stop the current piece

                if (Direction == Axes.x)
                {
                    SpawnFallingPieceX();
                    AdjustStandingPieceX();
                    prevPiece = newPiece;

                    LeavePieceZ(); // Instantiate a new piece on Z axis
                }
                else // Direction == Axes.z
                {
                    SpawnFallingPieceZ();
                    AdjustStandingPieceZ();
                    prevPiece = newPiece;

                    LeavePieceX(); // Instantiate a new piece on X axis
                }

                ChangeAxis(); // Change the axis
            }
        }
     }

    // Initializing piece and scene
    private void InitializeGameAtStart()
    {
        prevPiece = GameObject.FindWithTag("Ground");
        newPiece = Instantiate(piecePrefab, new Vector3(6.99f, towerHeight, 0), Quaternion.identity, transform); // Instantiate the first piece
        SetTowerHeight();
    }

    // Instantiates a new piece on X axis
    private void LeavePieceX()
    {
        newPiece = Instantiate(piecePrefab, new Vector3(6.99f, towerHeight, prevPiece.transform.localPosition.z), Quaternion.identity, transform);
        newPiece.transform.localPosition = new Vector3(newPiece.transform.position.x, newPiece.transform.position.y, newPiecePosition.z);
        newPiece.transform.localScale = newPieceScale;
    }

    // Instantiates a new piece on Z axis
    private void LeavePieceZ()
    {
        newPiece = Instantiate(piecePrefab, new Vector3(prevPiece.transform.localPosition.x, towerHeight, 6.99f), Quaternion.identity, transform);
        newPiece.transform.localPosition = new Vector3(newPiecePosition.x, newPiece.transform.position.y, newPiece.transform.position.z);
        newPiece.transform.localScale = newPieceScale;
    }

    // Instantiate and rescale falling piece moving on X axis
    void SpawnFallingPieceX()
    {
        fallingPiece = Instantiate(fallingPiecePrefab, fallingPieceContainerTransform);

        fallingPieceScale = prevPiece.transform.localScale;
        fallingPieceScale.x = Mathf.Abs(prevPiece.transform.localPosition.x - newPiece.transform.localPosition.x);
        fallingPiece.transform.localScale = fallingPieceScale;

        fallingPiecePosition = newPiece.transform.localPosition;
        fallingPiecePosition.x = newPiece.transform.localPosition.x > prevPiece.transform.localPosition.x
                               ? prevPiece.transform.localPosition.x + (prevPiece.transform.localScale.x + fallingPiece.transform.localScale.x) / 2
                               : prevPiece.transform.localPosition.x - (prevPiece.transform.localScale.x + fallingPiece.transform.localScale.x) / 2;
        fallingPiece.transform.localPosition = fallingPiecePosition;

        fallingPiece.AddComponent<Rigidbody>().mass = 10; // Add Rigidbody.mass to make object falling
    }

    void AdjustStandingPieceX()
    {
        float xDistance = Mathf.Abs(prevPiece.transform.localPosition.x - newPiece.transform.localPosition.x);
        newPieceScale = prevPiece.transform.localScale;
        newPieceScale.x = Mathf.Abs(newPieceScale.x - xDistance);
        newPiece.transform.localScale = newPieceScale;

        newPiecePosition = newPiece.transform.localPosition;
        newPiecePosition.x = newPiece.transform.localPosition.x > prevPiece.transform.localPosition.x
                           ? prevPiece.transform.localPosition.x + fallingPiece.transform.localScale.x / 2
                           : prevPiece.transform.localPosition.x - fallingPiece.transform.localScale.x / 2;
        newPiece.transform.localPosition = newPiecePosition;
    }

    // Instantiate and rescale falling piece moving on Z axis
    void SpawnFallingPieceZ()
    {
        fallingPiece = Instantiate(fallingPiecePrefab, fallingPieceContainerTransform);

        fallingPieceScale = prevPiece.transform.localScale;
        fallingPieceScale.z = Mathf.Abs(prevPiece.transform.localPosition.z - newPiece.transform.localPosition.z);
        fallingPiece.transform.localScale = fallingPieceScale;

        fallingPiecePosition = newPiece.transform.localPosition;
        fallingPiecePosition.z = newPiece.transform.localPosition.z > prevPiece.transform.localPosition.z
                               ? prevPiece.transform.localPosition.z + (prevPiece.transform.localScale.z + fallingPiece.transform.localScale.z) / 2
                               : prevPiece.transform.localPosition.z - (prevPiece.transform.localScale.z + fallingPiece.transform.localScale.z) / 2;
        fallingPiece.transform.localPosition = fallingPiecePosition;

        fallingPiece.AddComponent<Rigidbody>().mass = 10; // Add Rigidbody.mass to make object falling
    }

    void AdjustStandingPieceZ()
    {
        float zDistance = Mathf.Abs(prevPiece.transform.localPosition.z - newPiece.transform.localPosition.z);
        newPieceScale = prevPiece.transform.localScale;
        newPieceScale.z = Mathf.Abs(newPieceScale.z - zDistance);
        newPiece.transform.localScale = newPieceScale;

        newPiecePosition = newPiece.transform.localPosition;
        newPiecePosition.z = newPiece.transform.localPosition.z > prevPiece.transform.localPosition.z
                           ? prevPiece.transform.localPosition.z + fallingPiece.transform.localScale.z / 2
                           : prevPiece.transform.localPosition.z - fallingPiece.transform.localScale.z / 2;
        newPiece.transform.localPosition = newPiecePosition;
    }

    private bool IsOutOfPrevPiece()
    {
        if (Direction == Axes.x)
        {
            float prevPositiveXEdge = prevPiece.transform.localPosition.x + prevPiece.transform.localScale.x / 2;
            float prevNegativeXEdge = prevPiece.transform.localPosition.x - prevPiece.transform.localScale.x / 2;

            float newPositiveXEdge = newPiece.transform.localPosition.x + newPiece.transform.localScale.x / 2;
            float newNegativeXEdge = newPiece.transform.localPosition.x - newPiece.transform.localScale.x / 2;

            if (newNegativeXEdge > prevPositiveXEdge || newPositiveXEdge < prevNegativeXEdge)
                return true;
            else
                return false;
        }
        else // Direction == Axes.z
        {
            float prevPositiveZEdge = prevPiece.transform.localPosition.z + prevPiece.transform.localScale.z / 2;
            float prevNegativeZEdge = prevPiece.transform.localPosition.z - prevPiece.transform.localScale.z / 2;

            float newPositiveZEdge = newPiece.transform.localPosition.z + newPiece.transform.localScale.z / 2;
            float newNegativeZEdge = newPiece.transform.localPosition.z - newPiece.transform.localScale.z / 2;

            if (newNegativeZEdge > prevPositiveZEdge || newPositiveZEdge < prevNegativeZEdge)
                return true;
            else
                return false;
        }
    }

    private void OnGameOver()
    {
        PieceMovement.Instance.StopPiece();
        newPiece.GetComponent<Rigidbody>().isKinematic = false;
        newPiece.GetComponent<Rigidbody>().useGravity = true;
        newPiece.GetComponent<Rigidbody>().mass = 10;
    }

    // Sets tower height depends on chilCount of the object
    private void SetTowerHeight()
    {
        towerHeight = transform.childCount * piecePrefab.transform.localScale.y; // Scale of the piece on the Y axis is 0.5f
    }

    // Chages directions between X and Z
    private void ChangeAxis()
    {
        if (Direction == Axes.x)
            Direction = Axes.z;
        else
            Direction = Axes.x;
    }
}
