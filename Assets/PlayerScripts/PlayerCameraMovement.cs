using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private float sensX = 1f;
    [SerializeField] private float sensY = 1f;

    [SerializeField] Transform cam = null;
    [SerializeField] Transform orientation = null;

    float mouseX;
    float mouseY;
    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX;
        xRotation -= mouseY * sensY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //make up and down view clamped

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); //rotate camera
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0); //rotate player orientation side to side
    }
}
