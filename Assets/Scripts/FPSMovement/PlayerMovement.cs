using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    public float gravity = 10f;
    public float jumpSpeed = 2f;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;

    CharacterController charController;
    float horizontal, vertical;
    Vector3 movement;
    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Reset vertical velocity
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        // Move with keyboard input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = transform.forward * vertical + transform.right * horizontal;
        charController.Move(movement * speed * Time.deltaTime);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpSpeed;
        }

        // Gravity
        velocity.y -= gravity * Time.deltaTime;

        // Apply velocity
        charController.Move(velocity);
    }
}
