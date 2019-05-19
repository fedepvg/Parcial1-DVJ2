using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected enum Directions
    {
        forward, back, left, right, none,
    }

    protected float Speed;
    Vector2 Movement;
    protected Directions MoveDir;
    Directions TurnAlignTo = Directions.none;
    const float CenterPosition = 0.5f;
    const float UpperLimitForTurn = 0.55f;
    const float LowerLimitForTurn = 0.45f;
    protected bool[] PosibleMovement = new bool[(int)Directions.none];
    public LayerMask RaycastLayer;
    const float RayDistance = 0.51f;

    private void Start()
    {
        MoveDir = Directions.forward;
        for(int i=0;i<(int)Directions.none; i++)
        {
            PosibleMovement[i] = true;
        }
    }

    protected void CheckSnapping()
    {
        if (!CheckOnCorner())
        {
            if (MoveDir == Directions.forward || MoveDir == Directions.back)
            {
                SetX();

            }
            else if (MoveDir == Directions.right || MoveDir == Directions.left)
            {
                SetZ();
            }
        }
        else
        {
            TurnOnCorner();
            TurnAlignTo = Directions.none;
        }
    }

    void SetX()
    {
        transform.position = new Vector3(Mathf.CeilToInt(transform.position.x) - CenterPosition, transform.position.y, transform.position.z);
    }

    void SetZ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.CeilToInt(transform.position.z) - CenterPosition);
    }

    bool CheckOnCorner()
    {
        float roundedX = transform.position.x - Mathf.FloorToInt(transform.position.x);
        float roundedZ = transform.position.z - Mathf.FloorToInt(transform.position.z);
        switch (MoveDir)
        {
            case Directions.back:
            case Directions.forward:
                if (roundedX > UpperLimitForTurn)
                {
                    TurnAlignTo = Directions.left;
                    return true;
                }
                else if (roundedX < LowerLimitForTurn)
                {
                    TurnAlignTo = Directions.right;
                    return true;
                }
                else
                {
                    TurnAlignTo = Directions.none;
                    return false;
                }
            case Directions.right:
            case Directions.left:
                if (roundedZ > UpperLimitForTurn)
                {
                    TurnAlignTo = Directions.back;
                    return true;
                }
                else if (roundedZ < LowerLimitForTurn)
                {
                    TurnAlignTo = Directions.forward;
                    return true;
                }
                else
                {
                    TurnAlignTo = Directions.none;
                    return false;
                }
            default:
                return false;
        }
    }

    void TurnOnCorner()
    {
        switch(TurnAlignTo)
        {
            case Directions.forward:
                transform.position += Vector3.forward * Speed * Time.deltaTime;
                break;
            case Directions.back:
                transform.position += Vector3.back * Speed * Time.deltaTime;
                break;
            case Directions.left:
                transform.position += Vector3.left * Speed * Time.deltaTime;
                break;
            case Directions.right:
                transform.position += Vector3.right * Speed * Time.deltaTime;
                break;
        }
    }

    virtual public void CheckPosibleMovement()
    {
        RaycastHit hit;
        string layerHitted;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, RayDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Wall" || layerHitted == "Bomb")
            {
                PosibleMovement[(int)Directions.forward] = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.forward * RayDistance, Color.yellow);
            PosibleMovement[(int)Directions.forward] = true;
        }
        if (Physics.Raycast(transform.position, Vector3.back, out hit, RayDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Wall" || layerHitted == "Bomb")
            {
                PosibleMovement[(int)Directions.back] = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.back * RayDistance, Color.yellow);
            PosibleMovement[(int)Directions.back] = true;
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hit, RayDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Wall" || layerHitted == "Bomb")
            {
                PosibleMovement[(int)Directions.left] = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.left * RayDistance, Color.yellow);
            PosibleMovement[(int)Directions.left] = true;
        }
        if (Physics.Raycast(transform.position, Vector3.right, out hit, RayDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Wall" || layerHitted == "Bomb")
            {
                PosibleMovement[(int)Directions.right] = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.right * RayDistance, Color.yellow);
            PosibleMovement[(int)Directions.right] = true;
        }
    }
}
