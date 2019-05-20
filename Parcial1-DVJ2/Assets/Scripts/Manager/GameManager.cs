using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    Spawner spawner=new Spawner();
    private int score;
    int ScorePerEnemyDown = 100;
    int ScorePerPlayerDown = -50;
    int ScorePerLevelFinished = 200;
    int lives = 2;
    int MaxLives = 2;
    public GameObject ClosedDoorPrefab;
    public GameObject OpenedDoorPrefab;
    public GameObject ClosedDoor;
    public GameObject OpenedDoor;
    string HighScoreKey = "HiScore";

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        lives = MaxLives;
        score = 0;
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public Spawner Spawner
    {
        get { return spawner; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }


    public int Lives
    {
        get { return lives; }
    }

    private void AddScore(int s)
    {
        score += s;
    }

    public void EnemyKilled()
    {
        AddScore(ScorePerEnemyDown);
    }

    public void PlayerKilled()
    {
        AddScore(ScorePerPlayerDown);
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LevelFinished()
    {
        AddScore(ScorePerLevelFinished);
    }

    public void GameOver()
    {
        LoaderManager.Instance.LoadScene("EndGameScene");
        UILoadingScreen.Instance.SetVisible(true);
        if(!PlayerPrefs.HasKey(HighScoreKey))
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }
        else if(PlayerPrefs.GetInt(HighScoreKey) < score)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }
    }

    public void OpenDoor()
    {
        OpenedDoor = Instantiate(OpenedDoorPrefab, ClosedDoor.transform.position, Quaternion.identity);
        Destroy(ClosedDoor);
    }
}
