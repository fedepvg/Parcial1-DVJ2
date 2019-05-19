using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestWallsSpawner : MonoBehaviour
{
    public GameObject DestWallPrefab;
    const int Quantity = 60;
    public Collider Floor;

    void Start()
    {
        List<Vector3> positions = new List<Vector3>();
        GameObject go;
        for (int i=0;i<Quantity;i++)
        {
            go = Instantiate(DestWallPrefab);
            go.transform.position = GameManager.Instance.Spawner.RandomPositionOnLevel(go);
            positions.Add(go.transform.position);
            go.transform.SetParent(transform);
            go.name = "DestWall" + i;
        }
    }
}
