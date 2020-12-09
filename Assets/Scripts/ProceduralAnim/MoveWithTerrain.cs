using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithTerrain : MonoBehaviour
{
    public LayerMask groundLayer;

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 5f, Vector3.down, out hit, 10f, groundLayer))
        {
            transform.position = hit.point;
        }       
    }
}
