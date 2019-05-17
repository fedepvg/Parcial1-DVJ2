using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Directions
{
    forward,back,left,right,none,
}

public class Character : MonoBehaviour
{
    const float Speed = 2f;
    Vector2 Movement;
    Directions MoveDir;
    Directions TurnDirection=Directions.none;

    private void Start()
    {
        MoveDir = Directions.forward;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (Mathf.FloorToInt(transform.position.x) % 2 == 0)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.position += Vector3.forward * Speed * Time.deltaTime;
                    MoveDir = Directions.forward;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.position += Vector3.back * Speed * Time.deltaTime;
                    MoveDir = Directions.back;
                }
            }
            if (Mathf.FloorToInt(transform.position.z) % 2 == 0)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.position += Vector3.left * Speed * Time.deltaTime;
                    MoveDir = Directions.left;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.position += Vector3.right * Speed * Time.deltaTime;
                    MoveDir = Directions.right;
                }
            }
            CheckSnapping();
        }
        else
        {

            MoveDir = Directions.none;
        }
        
    }

    void CheckSnapping()
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
            TurnDirection = Directions.none;
        }
    }

    void SetX()
    {
        transform.position = new Vector3(Mathf.CeilToInt(transform.position.x) - 0.5f, transform.position.y, transform.position.z);
    }

    void SetZ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.CeilToInt(transform.position.z) - 0.5f);
    }

    bool CheckOnCorner()
    {
        float roundedX = transform.position.x - Mathf.FloorToInt(transform.position.x);
        float roundedZ = transform.position.z - Mathf.FloorToInt(transform.position.z);
        switch (MoveDir)
        {
            case Directions.back:
            case Directions.forward:
                if (roundedX > 0.65f)
                {
                    TurnDirection = Directions.left;
                    return true;
                }
                else if (roundedX < 0.35f)
                {
                    TurnDirection = Directions.right;
                    return true;
                }
                else
                {
                    TurnDirection = Directions.none;
                    return false;
                }
            case Directions.right:
            case Directions.left:
                if (roundedZ > 0.65f)
                {
                    TurnDirection = Directions.back;
                    return true;
                }
                else if (roundedZ < 0.35f)
                {
                    TurnDirection = Directions.forward;
                    return true;
                }
                else
                {
                    TurnDirection = Directions.none;
                    return false;
                }
            default:
                return false;
        }
    }

    void TurnOnCorner()
    {
        switch(TurnDirection)
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
}
