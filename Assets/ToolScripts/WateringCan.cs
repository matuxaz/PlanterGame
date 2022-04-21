using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public Camera cam;
    public GameObject water;
    public Transform firePoint;
    public Transform splashPoint;

    public float speed = 10;
    public float fireRate = 20;


    private float timeToFire;
    private Vector3 destination;

    PlayerMovement pm;
    private bool wasGrounded = true;
    public GameObject splashVfx;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>(); //refference to get isGrounded from PlayerMovement
    }

    private void Update()
    {

        if (wasGrounded == false && pm.isGrounded == true)
        {
            splash();
        }
        wasGrounded = pm.isGrounded;


        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate; 
            Shoot();
        }
    }

    private void splash()
    {
        GameObject spVfx = Instantiate(splashVfx, splashPoint.position, splashPoint.rotation); //creating and destroying explosion
        Destroy(spVfx, 1);
    }

    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        InstantiateWater(firePoint);
    }

    private void InstantiateWater(Transform firePoint)
    {
        var projectileObj = Instantiate(water, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * speed;
    }
}
