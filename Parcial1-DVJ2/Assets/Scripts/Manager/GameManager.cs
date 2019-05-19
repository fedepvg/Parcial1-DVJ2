using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    Spawner spawner=new Spawner();

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        spawner.Initialize();
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public Spawner Spawner
    {
        get { return spawner; }
    }
}
