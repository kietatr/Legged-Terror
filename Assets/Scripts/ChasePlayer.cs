using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float speed = 4f;
    public float rotateSpeed = 1.5f;

    GameObject player;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Vector3 moveDir = player.transform.position - rb.position;
        moveDir.Normalize();
        rb.angularVelocity = Vector3.Cross(transform.forward, moveDir) * rotateSpeed;
        rb.AddForce(transform.forward * speed);
    }
}
