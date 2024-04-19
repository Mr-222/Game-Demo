using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyEvent : MonoBehaviour
{
    public GameObject spawnPrefab;
    public LayerMask groundLayer;

    void OnDestroy()
    {
        SpawnVenomAtGround(transform.position);
    }

    private void SpawnVenomAtGround(Vector3 impactPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(impactPoint + Vector3.up * 0.5f, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 groundPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Instantiate(spawnPrefab, groundPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(spawnPrefab, impactPoint, Quaternion.identity);
        }
    }
}
