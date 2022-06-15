using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    [Tooltip("How sensitive should the mouse be? Default is: 300")]
    public float mouseSensitivity = 300f;               //Public variable for mouse sensitivity

    private Camera mainCamera;
    private float xRotation;

    void Start()
    {
        mainCamera = Camera.main;                           //Fetches Main Camera in scene
        //Cursor.lockState = CursorLockMode.Locked;       //Locks mouse to middle of screen
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;        //Moves Mouse's X Axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;        //Moves Mouse's Y Axis

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }
}
