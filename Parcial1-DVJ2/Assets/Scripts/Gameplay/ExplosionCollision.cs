using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DestWall")
        {
            Destroy(gameObject);
        }
    }
}
