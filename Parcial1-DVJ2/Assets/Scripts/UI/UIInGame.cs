using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    int KilledEnemies;
    int Score;
    int Lives;
    public Text KilledEnemiesText;
    public Text ScoreText;
    public Text LivesText;

    private void Update()
    {
        int ActualScore = GameManager.Instance.Score;
        int ActualLives = GameManager.Instance.Lives;
        int ActualEnemiesKilled = EnemyManager.Instance.EnemiesDown;

        if (ActualEnemiesKilled != KilledEnemies)
        {
            KilledEnemies = ActualEnemiesKilled;
            KilledEnemiesText.text = "Killed Enemies: " + KilledEnemies;
        }

        if (ActualScore != Score)
        {
            Score = ActualScore;
            ScoreText.text = "Score: " + Score;
        }

        if (ActualLives != Lives)
        {
            Lives = ActualLives;
            LivesText.text = "Lives: " + Lives;
        }
    }
}
