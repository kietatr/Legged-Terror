using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraWithMouse : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;

    float mouseX, mouseY;
    float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        transform.localRotation = Quaternion.Euler(Mathf.Clamp(xRotation, -45f, 45f), 0f, 0f);
        player.Rotate(mouseX * Vector3.up);    
    }
}
