using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSway : MonoBehaviour
{
    public float smoothness = 8;
    public float swayMultiplier = 4;

    public Vector3 defaultPosition = new Vector3(0.7f, -0.7f, 0.9f);
    public Vector3 walkEndPosition = new Vector3(0.7f, -0.9f, 0.9f);

    public Quaternion defaultRotation = Quaternion.Euler(Vector3.zero);
    public Quaternion sprintRotation = Quaternion.Euler(new Vector3(45, -45, 0));


    public float walkAnimationDuration = 0.2f;
    public float sprintAnimationDuration = 0.3f;
    public float elapsedTime;
    public float sprintTime;

    public bool walking = false;
    public bool sprinting = false;

    PlayerMovement pm;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>(); //refference to get isGrounded from PlayerMovement
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f) //walking
        {
            StartWalk();
        }
        else EndWalk();

        if (Input.GetKey(KeyCode.LeftShift) && pm.isGrounded && walking) //sprinting, on ground and walking
        {
            StartSprint();
        }
        else EndSprint();

        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion rotation = rotationX * rotationY;

        if (!sprinting) //sway animation based on mouse movement
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, smoothness * Time.deltaTime);
        }
        

    }

    void StartWalk()
    {
        if (!walking) //if haven't walked before start walking animation
        {
            elapsedTime = 0;
        }
        walking = true;

        elapsedTime += Time.deltaTime;
        float walkPercentage = elapsedTime / walkAnimationDuration;
        transform.localPosition = Vector3.Lerp(defaultPosition, walkEndPosition, Mathf.SmoothStep(0, 1, walkPercentage));

    }
    void EndWalk()
    {
        if (walking) //if just stopped walking start animation
        {
            elapsedTime = 0;
        }
        walking = false;

        elapsedTime += Time.deltaTime;
        float walkPercentage = elapsedTime / walkAnimationDuration;
        transform.localPosition = Vector3.Lerp(walkEndPosition, defaultPosition, Mathf.SmoothStep(0, 1, walkPercentage));
    }

    void StartSprint() //if haven't sprinted before start sprinting animation
    {
        if (!sprinting)
        {
            sprintTime = 0;
        }
        sprinting = true;

        sprintTime += Time.deltaTime;
        float sprintPercentage = sprintTime / sprintAnimationDuration;
        transform.localRotation = Quaternion.Slerp(defaultRotation, sprintRotation, Mathf.SmoothStep(0, 1, sprintPercentage));
    }
    void EndSprint() //if just stopped sprinting start animation
    {
        if (sprinting)
        {
            sprintTime = 0;
        }
        sprinting = false;

        sprintTime += Time.deltaTime;
        float sprintPercentage = sprintTime / sprintAnimationDuration;
        if(sprintPercentage < 1)
        {
            transform.localRotation = Quaternion.Slerp(sprintRotation, defaultRotation, Mathf.SmoothStep(0, 1, sprintPercentage));
        }
        
    }

}
