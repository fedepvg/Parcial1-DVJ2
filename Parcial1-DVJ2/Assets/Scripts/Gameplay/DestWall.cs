using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fire")
        {
            Destroy(gameObject);
        }
    }
}
