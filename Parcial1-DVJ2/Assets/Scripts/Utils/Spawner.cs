using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    public Collider Floor;
    int FloorOffset = 1;
    float PathAlignment = 0.5f;
    List<Vector3> PositionsTaken = new List<Vector3>();
    List<Vector3> EnemiesPositionsTaken = new List<Vector3>();
    Vector3 PlayerPos;
    float DistanceBetweenCharacters = 5;
    float Unit = 1;

    public void Initialize()
    {
        if(Floor==null)
            Floor = GameObject.Find("Floor").GetComponent<Collider>();
        PositionsTaken.Clear();
        EnemiesPositionsTaken.Clear();
    }

    public Vector3 RandomPositionOnLevel(GameObject go)
    {
        Vector3 pos;
        do
        {
            pos = new Vector3(Random.Range((int)Floor.bounds.min.x, (int)Floor.bounds.max.x - FloorOffset) + PathAlignment,
                                            Floor.bounds.center.y + go.transform.localScale.y / 2,
                                            Random.Range((int)Floor.bounds.min.z, (int)Floor.bounds.max.z - FloorOffset) + PathAlignment);
        } while (!IsPositionCorrect(pos, PositionsTaken, go) || !IsDistanceCorrect(go,pos));
        if (go.tag == "Player")
        {
            PlayerPos = pos;
            PositionsTaken.Add(pos + Vector3.forward);
            PositionsTaken.Add(pos + Vector3.back);
            PositionsTaken.Add(pos + Vector3.left);
            PositionsTaken.Add(pos + Vector3.right);
        }
        if (go.tag == "Enemy")
            EnemiesPositionsTaken.Add(pos);
        PositionsTaken.Add(pos);
        return pos;
    }

    bool IsPositionCorrect(Vector3 actualPos, List<Vector3> positions, GameObject go)
    {
        if (Mathf.FloorToInt(actualPos.x) % 2 != 0 && Mathf.FloorToInt(actualPos.z) % 2 != 0)
        {
            return false;
        }
        foreach (Vector3 element in positions)
        {
            if (element.x == actualPos.x && element.z == actualPos.z)
            {
                return false;
            }

            if(go.tag == "Player")
            {
                if(AreDestWallsAround(element,actualPos))
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool AreDestWallsAround(Vector3 element, Vector3 actualPos)
    {
        if ((element.x + Unit == actualPos.x && element.z == actualPos.z)|| (element.x - Unit == actualPos.x && element.z == actualPos.z )||
            (element.z + Unit == actualPos.z && element.x == actualPos.x) || (element.z - Unit == actualPos.z && element.x == actualPos.x))
            return true;
        else
            return false;
    }

    bool IsDistanceCorrect(GameObject go,Vector3 pos)
    {
        if(go.tag == "Player")
        {
            foreach(Vector3 element in EnemiesPositionsTaken)
            {
                if (Vector3.Distance(pos,element) < DistanceBetweenCharacters)
                {
                    return false;
                }
            }
        }
        else if(go.tag == "Enemy")
        {
            if (PlayerPos != null)
            {
                if (Vector3.Distance(pos, PlayerPos) < DistanceBetweenCharacters)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
