using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    const float speed = 2f;
    public GameObject BombPrefab;

    private void Start()
    {
        Speed = speed;
        transform.position = GameManager.Instance.Spawner.RandomPositionOnLevel(gameObject);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            CheckPosibleMovement();
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && PosibleMovement[(int)Directions.forward])
            {
                transform.position += Vector3.forward * Speed * Time.deltaTime;
                MoveDir = Directions.forward;
            }
            else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && PosibleMovement[(int)Directions.back])
            {
                transform.position += Vector3.back * Speed * Time.deltaTime;
                MoveDir = Directions.back;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && PosibleMovement[(int)Directions.left])
            {
                transform.position += Vector3.left * Speed * Time.deltaTime;
                MoveDir = Directions.left;
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && PosibleMovement[(int)Directions.right])
            {
                transform.position += Vector3.right * Speed * Time.deltaTime;
                MoveDir = Directions.right;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Instantiate(BombPrefab, transform.position, Quaternion.identity);
            }
            CheckSnapping();
        }
        else
        {

            MoveDir = Directions.none;
        }

    }
}
