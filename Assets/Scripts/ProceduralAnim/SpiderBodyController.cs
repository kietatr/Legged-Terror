using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBodyController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 0.5f;

    public Vector3 rotationAngles;

    void Update()
    {
        float upDown = Input.GetAxisRaw("Vertical");
        float leftRight = Input.GetAxisRaw("Horizontal");
        // float upDown =  Mathf.Ceil(Random.Range(-1f, 1f));
        // float leftRight = Mathf.Ceil(Random.Range(-1f, 1f));

        if (upDown <= -0.1f || upDown >= 0.1f)
        {
            Vector3 direction = transform.forward * upDown;
            Vector3 velocity = direction * speed;
            Vector3 moveAmount = velocity * Time.deltaTime;
            transform.position += moveAmount;
        }

        if (leftRight <= -0.1f || leftRight >= 0.1f)
        {
            rotationAngles = new Vector3(0, rotationSpeed * leftRight, 0);
            transform.Rotate(rotationAngles, Space.Self);
        }
    }
}
