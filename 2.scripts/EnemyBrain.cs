using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    float initialPos;

    public float speed;

    public int acidCount = 0;
    public int health = 100;

    public bool lookPlayer = false;
    public bool turnLeft = false;
    public bool canSpawn = false;

    public GameObject player;
    public GameObject ray;
    public GameObject acidBall;

    void Start()
    {
        initialPos = transform.position.x;
        turnLeft = true;
    }

    void Update()
    {
        if(!lookPlayer)
        {
            Turner();
            Movement();
        }
        if (lookPlayer)
        {
            LookPlayer();
            if(canSpawn)
            {
                StartCoroutine(SpawnAcid());
            }
        }
        Raycast();
    }
    void Movement()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
    void Turner()
    {
        if(transform.position.x > initialPos + 10)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if (transform.position.x < initialPos - 10)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    void Raycast()
    {
        RaycastHit hit;

        if(Physics.Raycast(ray.transform.position,ray.transform.right,out hit,5f))
        {
            if(!lookPlayer)
            {
                if (hit.collider.tag == "Platform" && turnLeft)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                    turnLeft = false;
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                    turnLeft = true;
                }
                Debug.Log(hit.collider.name);
            }
            if(hit.collider.tag=="Player")
            {
                lookPlayer = true;
            }
        }
    }
    void LookPlayer()
    {
        transform.LookAt(player.transform.position);
    }
    IEnumerator SpawnAcid()
    {
        Instantiate(acidBall, transform.position, transform.rotation);
        canSpawn = false;
        yield return new WaitForSeconds(8f);
        canSpawn = true;
    }
}
