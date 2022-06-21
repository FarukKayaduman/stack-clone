using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private GameObject leavePieceButton;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    private int score = 0;

    private void Start()
    {
        restartMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            score = PieceManager.Instance.transform.childCount;
            SetScoreText();
        }

        if (PieceManager.Instance.isGameEnded)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        PieceManager.Instance.newPiece.SetActive(true);
        mainMenu.SetActive(false);
        scoreText.gameObject.SetActive(true);
        leavePieceButton.SetActive(true);
    }

    public void GameOver()
    {
        UpdateHighScore();
        restartMenu.SetActive(true);
        leavePieceButton.SetActive(false);
    }

    public void RestartToMainMenu()
    {
        scoreText.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        restartMenu.SetActive(false);
    }

    public void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", score);

        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
