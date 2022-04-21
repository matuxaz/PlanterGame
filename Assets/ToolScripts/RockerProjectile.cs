using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerProjectile : MonoBehaviour
{
    public GameObject explosionVfx;

    public float force = 2000;
    public float radius = 15;
    Vector3 position;
    Quaternion rotation;

    Rigidbody player;
    public LayerMask playerMask;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        Destroy(gameObject, 5); //destroy after 5 seconds if doesn't hit anything
    }

    private bool collided;
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag != "Bullet" && !collided)
        {
            collided = true;
            position = GetComponent<Rigidbody>().position;
            rotation = GetComponent<Rigidbody>().rotation;

            GameObject expVfx = Instantiate(explosionVfx, position, rotation); //creating and destroying explosion
            Destroy(expVfx, 5);

            Explosion();

            Destroy(gameObject);
        }
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        bool rocketJump = Physics.CheckSphere(position, radius, playerMask);

        if (rocketJump) //if player is in the radius, add force for a rocket jump
        {
            player.AddExplosionForce(5000, position, radius);
        }

        foreach(Collider nearObject in colliders) //find objects in the explosion and add force
        {
            Rigidbody r = nearObject.GetComponent<Rigidbody>();
            if(r != null)
            {
                r.AddExplosionForce(force, position, radius);
            }
        }
    }
}
