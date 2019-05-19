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

    private void Initialize()
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

    void AddScore(int s)
    {
        score += s;
        Debug.Log("Score: " + score);
    }

    public void EnemyKilled()
    {
        AddScore(ScorePerEnemyDown);
    }

    public void PlayerKilled()
    {
        AddScore(ScorePerPlayerDown);
        lives--;
        Debug.Log("Lives: " + lives);
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

    }

    public void OpenDoor()
    {
        OpenedDoor = Instantiate(OpenedDoorPrefab, ClosedDoor.transform.position, Quaternion.identity);
        Destroy(ClosedDoor);
    }
}
