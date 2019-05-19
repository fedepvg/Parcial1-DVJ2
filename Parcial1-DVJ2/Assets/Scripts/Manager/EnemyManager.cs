using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public GameObject EnemyPrefab;
    public int EnemiesQuantity;
    private List<Enemy> Enemies = new List<Enemy>();
    private int enemiesDown;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        enemiesDown = 0;
    }

    private void Start()
    {
        Enemy.OnEnemyKilled += KillEnemy;
    }

    public void Initialize()
    {
        enemiesDown = 0;
    }

    public void InitializeLevel()
    {
        CreateEnemies();
    }

    public static EnemyManager Instance
    {
        get { return instance; }
    }

    public void CreateEnemies()
    {
        for (int i = 0; i < EnemiesQuantity; i++)
        {
            GameObject go = Instantiate(EnemyPrefab);
            Enemy e = go.GetComponent<Enemy>();
            go.transform.position = GameManager.Instance.Spawner.RandomPositionOnLevel(go);
            Enemies.Add(e);
        }
    }

    void KillEnemy(GameObject e)
    {
        enemiesDown++;
        GameManager.Instance.EnemyKilled();
        Destroy(e);
        if(enemiesDown>=EnemiesQuantity)
        {
            GameManager.Instance.OpenDoor();
        }
    }

    public int EnemiesDown
    {
        get { return enemiesDown; }
    }
}
