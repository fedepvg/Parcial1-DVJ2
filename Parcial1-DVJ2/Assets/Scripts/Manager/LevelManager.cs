using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.Spawner.Initialize();
        EnemyManager.Instance.InitializeLevel();
    }
}
