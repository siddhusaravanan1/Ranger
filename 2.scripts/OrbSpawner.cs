using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject teleportOrb;

    public EnemyBrain _enemyBrain;
    public ShieldMonsterBehaviour _shieldMonsterBehaviour;
    void Start()
    {
        if (_enemyBrain.health == 0 || _shieldMonsterBehaviour.health == 0)
        {
            Instantiate(teleportOrb, transform.position, Quaternion.identity);
        }
    }
    void Update()
    {

    }
}
