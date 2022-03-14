using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform player;
    public float rotationSpeed = 3f;
    public float moveSpeed = 3f;
    public float viewDistance = 5f;
    public LayerMask playerMask;

    public float chooseTime = 3f;
    public float elapsedTime;

    public bool canFindPlayer;

    public float percentage;
    public Quaternion rotation;
    public Quaternion old;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        canFindPlayer = Physics.CheckSphere(transform.position, viewDistance, playerMask);
        if (canFindPlayer)
        {
            moveToPlayer();
        }
        else wander();
            
    }

    void moveToPlayer()
    {
        //rotation to player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), Time.deltaTime * rotationSpeed);

        //moving to player
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void wander()
    {
        elapsedTime += Time.deltaTime;
        percentage = elapsedTime / chooseTime;

        if (percentage >=1)
        {
            old = rotation;
            rotation = Random.rotation;
            elapsedTime = 0;
        }
        transform.localRotation = Quaternion.Slerp(old, rotation, percentage);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
