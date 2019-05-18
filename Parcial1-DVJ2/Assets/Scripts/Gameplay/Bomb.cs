using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    const float Alignment = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
        Invoke("Explode", 3f);
    }

    void Explode()
    {
        Destroy(this.gameObject);
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }

    public void ActivateCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    void SetPosition()
    {
        transform.position = new Vector3(Mathf.FloorToInt(transform.position.x) + Alignment, transform.position.y, Mathf.FloorToInt(transform.position.z) + Alignment);
    }
}
