using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5);
    }
    private bool collided;
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag != "Bullet" && !collided)
        {
            collided = true;
            Destroy(gameObject, 2);
        }
    }
}
