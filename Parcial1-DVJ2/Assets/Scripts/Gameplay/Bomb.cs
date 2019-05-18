using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    const float Alignment = 0.5f;
    private int ExplosionRange = 1;
    public LayerMask RaycastLayer;
    public float TimeToExplode = 3f;
    float TimeToDestroy;
    Collider Col;

    // Start is called before the first frame update
    void Start()
    {
        TimeToDestroy = TimeToExplode + 1;
        SetPosition();
        Invoke("Explode", TimeToExplode);
        Col = GetComponent<Collider>();
    }

    void Explode()
    {
        Destroy(this.gameObject,TimeToDestroy);
        transform.Find("Base").gameObject.SetActive(false);
        
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, this.transform);
        string layerHitted;
        RaycastHit hit;
        for(int i=1;i<=ExplosionRange;i++)
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, ExplosionRange, RaycastLayer))
            {
                layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);
                if(layerHitted!="Wall")
                    Instantiate(ExplosionPrefab, transform.position + Vector3.forward * i, Quaternion.identity, this.transform);
            }
            else
            {
                Instantiate(ExplosionPrefab, transform.position + Vector3.forward * i, Quaternion.identity, this.transform);
            }

            if (Physics.Raycast(transform.position, Vector3.back, out hit, ExplosionRange, RaycastLayer))
            {
                layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);
                if (layerHitted != "Wall")
                    Instantiate(ExplosionPrefab, transform.position + Vector3.back * i, Quaternion.identity, this.transform);
            }
            else
            {
                Instantiate(ExplosionPrefab, transform.position + Vector3.back * i, Quaternion.identity, this.transform);
            }

            if (Physics.Raycast(transform.position, Vector3.left, out hit, ExplosionRange, RaycastLayer))
            {
                layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);
                if (layerHitted != "Wall")
                    Instantiate(ExplosionPrefab, transform.position + Vector3.left * i, Quaternion.identity, this.transform);
            }
            else
            {
                Instantiate(ExplosionPrefab, transform.position + Vector3.left * i, Quaternion.identity, this.transform);
            }

            if (Physics.Raycast(transform.position, Vector3.right, out hit, ExplosionRange, RaycastLayer))
            {
                layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);
                if (layerHitted != "Wall")
                    Instantiate(ExplosionPrefab, transform.position + Vector3.right * i, Quaternion.identity, this.transform);
            }
            else
            {
                Instantiate(ExplosionPrefab, transform.position + Vector3.right * i, Quaternion.identity, this.transform);
            }
        }
        Col.enabled = false;
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
