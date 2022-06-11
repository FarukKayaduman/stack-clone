using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject prevPiece, newPiece, fallingPiece;

    [SerializeField] private Transform backPieceContainerTransform;

    private float towerHeight = 0; // Tower height is 0 at start

    private bool isGameEnded = false;

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

        if (!isGameEnded && Input.GetMouseButtonDown(0)) // Left click
        {
            PieceMovement.Instance.StopPiece(); // Stop the current piece

            if (Direction == Axes.x)
            {
                SpawnFallingPieceX();
                AdjustStandingPieceX();

                LeavePieceZ(); // Instantiate a new piece
            }
            else if (Direction == Axes.z)
            {
                LeavePieceX();
            }
            
            // TODO: Adjust piecePrefab scale after LeavePiece
            
            prevPiece = newPiece;
            ChangeAxis(); // Change the axis
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
    }

    // Instantiates a new piece on Z axis
    private void LeavePieceZ()
    {
        newPiece = Instantiate(piecePrefab, new Vector3(prevPiece.transform.localPosition.x, towerHeight, 6.99f), Quaternion.identity, transform);
    }

    // Instantiate back piece and rescale current piece on X axis
    void SpawnFallingPieceX()
    {
        fallingPiece = Instantiate(cubePrefab, backPieceContainerTransform);

        Vector3 backPieceScale = prevPiece.transform.localScale;
        backPieceScale.x = Mathf.Abs(prevPiece.transform.localPosition.x - newPiece.transform.localPosition.x);
        fallingPiece.transform.localScale = backPieceScale;

        Vector3 backPiecePosition = prevPiece.transform.localPosition;
        backPiecePosition.x = newPiece.transform.localPosition.x > prevPiece.transform.localPosition.x
                            ? prevPiece.transform.localPosition.x + (prevPiece.transform.localScale.x + fallingPiece.transform.localScale.x) / 2
                            : prevPiece.transform.localPosition.x - (prevPiece.transform.localScale.x + fallingPiece.transform.localScale.x) / 2;
        backPiecePosition.y = newPiece.transform.localPosition.y;

        fallingPiece.transform.localPosition = backPiecePosition;
        fallingPiece.AddComponent<Rigidbody>().mass = 10; // Add Rigidbody.mass to make object falling
    }

    void AdjustStandingPieceX()
    {
        float xDistance = Mathf.Abs(prevPiece.transform.localPosition.x - newPiece.transform.localPosition.x);
        Vector3 newPieceScale = prevPiece.transform.localScale;
        newPieceScale.x = Mathf.Abs(newPieceScale.x - xDistance);
        newPiece.transform.localScale = newPieceScale;

        Vector3 newPiecePosition = newPiece.transform.localPosition;
        newPiecePosition.x = newPiece.transform.localPosition.x > prevPiece.transform.localPosition.x
                           ? prevPiece.transform.localPosition.x + fallingPiece.transform.localScale.x / 2
                           : prevPiece.transform.localPosition.x - fallingPiece.transform.localScale.x / 2;
        newPiece.transform.localPosition = newPiecePosition;
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
