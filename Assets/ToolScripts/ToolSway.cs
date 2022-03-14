using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSway : MonoBehaviour
{
    public float smoothness = 8;
    public float swayMultiplier = 3;

    public Vector3 walkStartPosition = new Vector3(0.7f, -0.7f, 0.9f);
    public Vector3 walkEndPosition = new Vector3(0.7f, -0.9f, 0.9f);
    public float duration = 0.1f;
    public float elapsedTime;

    public bool walking = false;
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            StartWalk();
        }
        else EndWalk();
       

        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX + 90, Vector3.up);

        Quaternion rotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, smoothness * Time.deltaTime);

    }

    void StartWalk()
    {
        if (!walking)
        {
            elapsedTime = 0;
        }
        walking = true;

        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / duration;
        transform.localPosition = Vector3.Lerp(walkStartPosition, walkEndPosition, Mathf.SmoothStep(0, 1, percentage));

    }
    void EndWalk()
    {
        if (walking)
        {
            elapsedTime = 0;
        }
        walking = false;

        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / duration;
        transform.localPosition = Vector3.Lerp(walkEndPosition, walkStartPosition, Mathf.SmoothStep(0, 1, percentage));
    }

}
