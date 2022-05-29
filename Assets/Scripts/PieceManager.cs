using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private GameObject currentPiece, newPiece;

    public GameObject piecePrefab;

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
        UpdateTowerHeight();

        if (!isGameEnded && Input.GetKeyDown(KeyCode.Space)) // When pressed space button
        {
            PieceMovement.Instance.StopPiece(); // Stop the current piece
            LeavePiece(); // Instantiate a new piece
        }
    }

    // Initializing piece and scene
    private void InitializeGameAtStart()
    {
        Instantiate(piecePrefab, new Vector3(6.99f, towerHeight, 0), Quaternion.identity, transform); // Instantiate the first piece
        UpdateTowerHeight();
    }

    // Instantiates a new piece
    private void LeavePiece()
    {
        if (Direction == Axes.x)
        {
            ChangeDirection();
            newPiece = Instantiate(piecePrefab, new Vector3(0, towerHeight, 6.99f), Quaternion.identity, transform);
        }
        else if (Direction == Axes.z)
        {
            ChangeDirection();
            newPiece = Instantiate(piecePrefab, new Vector3(6.99f, towerHeight, 0), Quaternion.identity, transform);
        }
    }

    // Updates tower height depends on chilCount of the object
    private void UpdateTowerHeight()
    {
        towerHeight = transform.childCount * piecePrefab.transform.localScale.y; // Scale of the piece on the Y axis is 0.5f
    }

    // Chages directions between X and Z
    private void ChangeDirection()
    {
        if (Direction == Axes.x)
            Direction = Axes.z;
        else
            Direction = Axes.x;
    }
}
