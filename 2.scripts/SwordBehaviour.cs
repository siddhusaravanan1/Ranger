using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    public EnemyBrain _enemyBehaviour;
    public ShieldMonsterBehaviour _shieldMonsterBehaviour;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider cd)
    {
        if(cd.gameObject.tag=="Enemy")
        {
            _enemyBehaviour.health -= 50;
        }
        if (cd.gameObject.tag == "ShieldMonster")
        {
            _enemyBehaviour.health -= 40;
        }
    }
}
