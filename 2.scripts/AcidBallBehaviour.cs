using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBallBehaviour : MonoBehaviour
{
    Rigidbody rb;

    public GameObject player;

    public PlayerBehaviour _playerBehaviour;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 10;
    }

    void Update()
    {
        StartCoroutine(Destroy());
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance<5)
        {
            _playerBehaviour.health -= 15;
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
