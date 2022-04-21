using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public Camera cam;
    public GameObject rocket;
    public Transform firePoint;

    public float speed = 10;
    public float fireRate = 2;


    private float timeToFire;
    private Vector3 destination;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        InstantiateRocket(firePoint);
    }

    private void InstantiateRocket(Transform firePoint)
    {
        var projectileObj = Instantiate(rocket, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * speed;
    }
}
