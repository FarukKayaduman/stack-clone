using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public GameObject piecePrefab;

    int childCount = 0;
    float towerHeight = 1.25f;

    bool isGameEnded = false;

    public enum Axes {x, z}; // x refers to X axis, z refers to Z axis
    public Axes movingAxisOfTheBlock = Axes.x;

    public static PieceManager m_Instance = null;
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
    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        CameraController.Instance.UpdateCameraPosition();
        UpdateTowerHeight();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PieceMovement.instance.StopPiece();
            UpdateChildCount();

            if (!isGameEnded && movingAxisOfTheBlock == Axes.x && childCount % 2 == 0)
            {
                movingAxisOfTheBlock = Axes.z;
                GameObject newPiece = Instantiate(piecePrefab, new Vector3(0, towerHeight, 6.99f), Quaternion.identity, transform);
            }
            else if (!isGameEnded && movingAxisOfTheBlock == Axes.z && childCount % 2 == 1)
            {
                movingAxisOfTheBlock = Axes.x;
                GameObject newPiece = Instantiate(piecePrefab, new Vector3(6.99f, towerHeight, 0), Quaternion.identity, transform);
            }
        }
    }


    void InitializeGame()
    {
        Instantiate(piecePrefab, new Vector3(6.99f, towerHeight, 0), Quaternion.identity, transform); // Create the first piece
        UpdateChildCount();
        UpdateTowerHeight();
    }

    void UpdateChildCount()
    {
        childCount++;
    }

    void UpdateTowerHeight()
    {
        towerHeight = 1.25f + childCount * 0.5f;
    }
}
