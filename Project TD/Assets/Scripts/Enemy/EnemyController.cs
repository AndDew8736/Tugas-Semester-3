using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Refferences")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 2f;
    private Transform target;
    private int pathIdx = 0;
    void Start()
    {
        target = LevelManager.main.path[0];
        //makes the enemy starts at the begining
    }

    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIdx++;
            //after the enemy reaches a target position, makes it go to a new target position
            if (pathIdx == LevelManager.main.path.Length)
            {
                int enemyDmg = gameObject.GetComponent<EnemyHealth>().HP;
                LevelManager.main.playerTakesDamage(enemyDmg);
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIdx];
            }
            //if it has reached the end, player takes damage then destroy itself
        }
    }
    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        //gets the direction of the next target
        rb.velocity = direction * moveSpeed;
        //changes the movement towards the target
    }
}
