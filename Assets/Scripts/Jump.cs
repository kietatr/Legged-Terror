using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5000f;
    public float jumpSpeed = 500f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            MoveTo(target);
            target = null;
        }
    }

    void MoveTo(Transform target) 
    {
        if (rb.velocity.y > -0.01f && target != null)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            rb.AddForce(Vector3.up * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
            rb.AddForce(directionToTarget * moveSpeed * Time.deltaTime);
        }
    }

    // bool isGrounded = false;
    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }
}
