using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    int FinalScore;
    public Text ScoreText;
    int HighScore;
    public Text HiScoreText;
    GameManager gameManager;
    string HighScoreKey = "HiScore";

    void Awake()
    {
        if (!GameManager.Instance)
        {
            gameManager = new GameManager();
        }
        else
        {
            gameManager = GameManager.Instance;
        }
        FinalScore = gameManager.Score;
        if(PlayerPrefs.HasKey(HighScoreKey))
        {
            HighScore = PlayerPrefs.GetInt(HighScoreKey);
        }
    }

    private void OnEnable()
    {
        if(ScoreText)
            ScoreText.text = "Score: " + FinalScore.ToString();
        if (HiScoreText)
            HiScoreText.text = "High Score: " + HighScore.ToString();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
