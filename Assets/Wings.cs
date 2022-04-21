using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MonoBehaviour
{

    public Transform left;
    public Transform right;
    public Transform point;

    [SerializeField]
    private Vector3 wingRotation;
    [SerializeField]
    private float flapLength = 20;
    [SerializeField]
    private float flapSpeed = 10;
    public float i = 20;

    void Update()
    {

        if (i < flapLength / 2)
        {
            wingRotation = new Vector3(0, 1f, 0);
        }
        else wingRotation = new Vector3(0, -1f, 0);

        //wingRotation = new Vector3(0, Mathf.Sin(flapFreq*i) * Time.deltaTime, 0);
        left.RotateAround(point.position, wingRotation, Time.deltaTime * flapSpeed);
        right.RotateAround(point.position, -wingRotation, Time.deltaTime * flapSpeed);

        i++;

        if(i >= flapLength)
        {
            i = 0;
        }
    }
}
