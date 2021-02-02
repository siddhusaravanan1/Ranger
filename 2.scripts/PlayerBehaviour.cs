using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody rb;

    public float speed;

    public GameObject sword;
    public GameObject enemy;
    public GameObject teleporter;

    public bool canWalk = false;
    public bool canAttack = false;
    public bool damage = false;

    bool canHeal = false;

    public int jumpCount = 0;
    public int dashSpeed = 0;
    public int health = 100;
    public int healthOrb = 0;
    public int teleportOrb = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canAttack = true;
        damage = true;
    }

    void Movement()
    {

        if(Input.GetKey(KeyCode.D))
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            rb.velocity = new Vector3(speed, 0, 0);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            rb.velocity = new Vector3(-speed, 0, 0);
        }
        if(Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector3.zero;
        }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3( 0 , 20 , 0 );
            jumpCount += 1;
        }
    }
    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            sword.SetActive(true); 
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            sword.SetActive(false);
        }
    }
    void Dodge()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.velocity = transform.right * dashSpeed;
            canAttack = false;
            StartCoroutine(DodgeHealth());
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            rb.velocity = Vector3.zero;
            canAttack = true;
            StartCoroutine(Damage());
        }
    }

    private void OnCollisionEnter(Collision cd)
    {
        if(cd.gameObject.tag=="Platform")
        {
            canWalk = true;
            StopCoroutine(CanWalk());
            jumpCount = 0;
        }
        if (cd.gameObject.tag == "ShieldMonster" && damage || cd.gameObject.tag == "Enemy" && damage)
        {
            health -= 25;
        }
    }
    private void OnCollisionStay(Collision cd)
    {
        if (cd.gameObject.tag == "Platform")
        {
            canWalk = true;
            StopCoroutine(CanWalk());
            jumpCount = 0;
        }
    }
    private void OnCollisionExit(Collision cd)
    {
        if (cd.gameObject.tag == "Platform")
        {
            StartCoroutine(CanWalk());
        }
    }
    private void OnTriggerEnter(Collider cd)
    {
        if (cd.gameObject.tag == "HealthOrb")
        {
            healthOrb += 1;
            canHeal = true;
        }
        if(cd.gameObject.tag=="TeleportOrb")
        {
            teleportOrb += 1;
            Destroy(cd.gameObject);
        }
        if(cd.gameObject.tag=="Death" && damage)
        {
            health -= 30;
        }

    }
    void HealthOrb()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            health = 100;
        }
    }
    IEnumerator CanWalk()
    {
        yield return new WaitForSeconds(2f);
        canWalk = false;
    }
    IEnumerator DodgeHealth()
    {
        damage = false;
        yield return new WaitForSeconds(0.5f);
        damage = true;
    }
    IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.5f);
        StopCoroutine(DodgeHealth());
    }
    void Update()
    {
        if (canWalk)
        {
            Movement();
            Dodge();
        }
        if (jumpCount < 1)
        {
            Jump();
        }
        if (canAttack)
        {
            Attack();
        }
        if (canHeal)
        {
            HealthOrb();
        }
        if (healthOrb == 0)
        {
            canHeal = false;
        }
        if(health == 100)
        {
            canHeal = false;
        }
        if (health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(teleportOrb>=5)
        {
            teleporter.SetActive(true);
        }
    }
}
