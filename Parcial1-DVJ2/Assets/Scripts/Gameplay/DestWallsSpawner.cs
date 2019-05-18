using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestWallsSpawner : MonoBehaviour
{
    public GameObject DestWallPrefab;
    const int Quantity = 60;
    public Collider Floor;
    int FloorOffset = 1;
    float PathAlignment = 0.5f;

    void Start()
    {
        List<Vector3> positions = new List<Vector3>();
        GameObject go;
        go = Instantiate(DestWallPrefab);
        go.transform.position = new Vector3(Random.Range((int)Floor.bounds.min.x, (int)Floor.bounds.max.x - FloorOffset) + PathAlignment,
                                            Floor.bounds.center.y + go.transform.localScale.y / 2,
                                            Random.Range((int)Floor.bounds.min.z, (int)Floor.bounds.max.z - FloorOffset) + PathAlignment);
        positions.Add(go.transform.position);
        go.transform.SetParent(transform);
        go.name = "DestWall" + 0;
        for (int i=1;i<Quantity;i++)
        {
            go = Instantiate(DestWallPrefab);
            do
                go.transform.position = new Vector3(Random.Range((int)Floor.bounds.min.x, (int)Floor.bounds.max.x - FloorOffset) + PathAlignment,
                                                    Floor.bounds.center.y + go.transform.localScale.y / 2,
                                                    Random.Range((int)Floor.bounds.min.z, (int)Floor.bounds.max.z - FloorOffset) + PathAlignment);
            while (!IsPositionCorrect(go.transform.position,positions));
            positions.Add(go.transform.position);
            go.transform.SetParent(transform);
            go.name = "DestWall" + i;
        }
    }

    bool IsPositionCorrect(Vector3 actualPos, List<Vector3>positions)
    {
        if(Mathf.FloorToInt(actualPos.x) % 2 != 0 && Mathf.FloorToInt(actualPos.z) % 2 !=0)
        {
            return false;
        }
        foreach(Vector3 element in positions)
        {
            if(element==actualPos)
            {
                return false;
            }
        }
        return true;
    }
}
