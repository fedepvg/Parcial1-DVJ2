using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    enum States
    {
        Idle, Chase, Last,
    }
    public delegate void EnemyKilledAction(GameObject e);
    public static EnemyKilledAction OnEnemyKilled;
    float speed = 3f;
    const float RayObstacleDistance = 0.51f;
    const float RayPlayerDistance = 2.51f;
    States ActualState;
    Directions PlayerDir;
    float ChangeDirTimer;
    const float TimeToChangeDir = 2f;

    private void Start()
    {
        Speed = speed;
        ActualState = States.Idle;
        ChangeDirTimer = 0f;
    }

    private void Update()
    {
        ChangeDirTimer += Time.deltaTime;
        CheckPosibleMovement();
        CheckPlayer();
        switch(MoveDir)
        {
            case Directions.forward:
                if (PosibleMovement[(int)Directions.forward])
                {
                    transform.position += Vector3.forward * Speed * Time.deltaTime;
                }
                else
                {
                    ChangeDirTimer = TimeToChangeDir;
                }
                break;
            case Directions.back:
                if (PosibleMovement[(int)Directions.back])
                { 
                    transform.position += Vector3.back * Speed * Time.deltaTime;
                }
                else
                {
                    ChangeDirTimer = TimeToChangeDir;
                }
                break;
            case Directions.left:
                if (PosibleMovement[(int)Directions.left])
                { 
                    transform.position += Vector3.left * Speed * Time.deltaTime;
                }
                else
                {
                    ChangeDirTimer = TimeToChangeDir;
                }
                break;
            case Directions.right:
                if (PosibleMovement[(int)Directions.right])
                { 
                    transform.position += Vector3.right * Speed * Time.deltaTime;
                }
                else
                {
                    ChangeDirTimer = TimeToChangeDir;
                }
                break;
            default:
                break;
        }

        switch(ActualState)
        {
            case States.Idle:
                if(ChangeDirTimer>=TimeToChangeDir)
                {
                    ChangeDirTimer = 0f;
                    SetRandDirection();
                }
                break;
            case States.Chase:
                ChangeDirTimer = 0f;
                SetNewDirection(PlayerDir);
                break;
            default:
                break;
        }
        CheckSnapping();
    }

    void EnemyKilled()
    {
        if (OnEnemyKilled != null)
            OnEnemyKilled(gameObject);
    }

    void SetRandDirection()
    {
        do
        {
            MoveDir = (Directions)Random.Range((int)Directions.forward, (int)Directions.none);
        } while (!PosibleMovement[(int)MoveDir]);
    }

    void SetNewDirection(Directions dir)
    {
        MoveDir = dir;
    }

    public void CheckPlayer()
    {
        RaycastHit hit;
        string layerHitted;
        ActualState = States.Idle;
        Physics.queriesHitTriggers = true;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, RayPlayerDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Player")
            {
                ActualState = States.Chase;
                PlayerDir = Directions.forward;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.forward * RayPlayerDistance, Color.white);
        }

        if (Physics.Raycast(transform.position, Vector3.back, out hit, RayPlayerDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Player")
            {
                ActualState = States.Chase;
                PlayerDir = Directions.back;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.back * RayPlayerDistance, Color.white);
        }

        if (Physics.Raycast(transform.position, Vector3.left, out hit, RayPlayerDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Player")
            {
                ActualState = States.Chase;
                PlayerDir = Directions.left;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.left * RayPlayerDistance, Color.white);
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, RayPlayerDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Player")
            {
                ActualState = States.Chase;
                PlayerDir = Directions.right;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.right * RayPlayerDistance, Color.white);
        }
    }

    public override void CheckPosibleMovement()
    {
        RaycastHit hit;
        string layerHitted;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, RayObstacleDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);
            
            if (layerHitted == "Wall" || layerHitted == "Bomb")
            {
                PosibleMovement[(int)Directions.forward] = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.forward * RayObstacleDistance, Color.yellow);
            PosibleMovement[(int)Directions.forward] = true;
        }

        if (Physics.Raycast(transform.position, Vector3.back, out hit, RayObstacleDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Wall" || layerHitted == "Bomb")
            {
                PosibleMovement[(int)Directions.back] = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.back * RayObstacleDistance, Color.yellow);
            PosibleMovement[(int)Directions.back] = true;
        }

        if (Physics.Raycast(transform.position, Vector3.left, out hit, RayObstacleDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Wall" || layerHitted == "Bomb")
            {
                PosibleMovement[(int)Directions.left] = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.left * RayObstacleDistance, Color.yellow);
            PosibleMovement[(int)Directions.left] = true;
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, RayObstacleDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Wall" || layerHitted == "Bomb")
            {
                PosibleMovement[(int)Directions.right] = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.right * RayObstacleDistance, Color.yellow);
            PosibleMovement[(int)Directions.right] = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Player":
                other.GetComponent<Player>().PlayerKilled();
                break;
            case "Enemy":
            case "Bomb":
                switch(MoveDir)
                {
                    case Directions.forward:
                        SetNewDirection(Directions.back);
                        break;
                    case Directions.back:
                        SetNewDirection(Directions.forward);
                        break;
                    case Directions.left:
                        SetNewDirection(Directions.right);
                        break;
                    case Directions.right:
                        SetNewDirection(Directions.left);
                        break;
                    default:
                        break;
                }
                break;
            case "Fire":
                EnemyKilled();
                break;
        }
    }
}
