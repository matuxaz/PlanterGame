using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform player;

    GameObject beeObject;
    GameObject bucket;
    GameObject blade;

    public float rotationSpeed = 4f;
    public float moveSpeed = 3f;
    public float viewDistance = 15f;
    public LayerMask playerMask;

    public float chooseTime = 2f;
    public float elapsedTime;

    public bool canFindPlayer;

    public float percentage;
    public Quaternion rotation;
    public Quaternion old;

    [SerializeField]
    private Texture defaultTexture;
    [SerializeField]
    private Texture angryTexture;
    [SerializeField]
    private Renderer beeRenderer; //for texture change

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        beeObject = GameObject.Find("BeeObject");

        bucket = beeObject.transform.GetChild(0).GetChild(0).gameObject;
        blade = beeObject.transform.GetChild(0).GetChild(1).gameObject;

    }
    void Update()
    {

        canFindPlayer = Physics.CheckSphere(transform.position, viewDistance, playerMask);
        if (canFindPlayer)
        {
            beeRenderer.material.mainTexture = angryTexture; //change texture and go to player if able to see him
            moveToPlayer();
        }
        else
        {
            beeRenderer.material.mainTexture = defaultTexture; //change texture and wander if cannot see player
            wander();
        }
            
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerBody")
        {
            player.GetComponent<Rigidbody>().AddExplosionForce(2000, transform.position - new Vector3(0f, 3f, 0f), 20);
        }
    }
    void moveToPlayer()
    {
        if (bucket.activeSelf)
        {
            bucket.SetActive(false);
            blade.SetActive(true);
        }
        //rotation to player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), Time.deltaTime * rotationSpeed);

        //moving to player
        transform.position += transform.forward * moveSpeed * 3 * Time.deltaTime;
        old = transform.rotation;
        elapsedTime = 0;
    }

    void wander()
    {
        if (blade.activeSelf)
        {
            bucket.SetActive(true);
            blade.SetActive(false);
        }

        elapsedTime += Time.deltaTime;
        percentage = elapsedTime / chooseTime;

        transform.localRotation = Quaternion.Slerp(old, rotation, percentage); //rotate randomly every fame
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        if (percentage > 1) //if ended rotation start new rotation
        {
            old = transform.localRotation;
            rotation = Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(0f, 360f), 0f);
            elapsedTime = 0;
        }
    }
}
