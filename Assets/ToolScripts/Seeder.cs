using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeder : MonoBehaviour
{
    public Camera cam;
    public GameObject seed;
    public Transform firePoint;

    public float speed = 50;
    public float fireRate = 15;


    private float timeToFire;
    private Vector3 destination;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) //find the crosshair position
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        InstantiateSeed(firePoint);
    }

    private void InstantiateSeed(Transform firePoint) //create a projectile at firepoint
    {
        var projectileObj = Instantiate(seed, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * speed;
    }
}
