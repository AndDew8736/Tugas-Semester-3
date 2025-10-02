using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Stats")]
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private int projectileDMG = 1;
    [SerializeField] private float projectileDuration = 5f;
    [SerializeField] private bool destroyOnCollision = true;
    private Transform target;
    public void setTarget(Transform _target)
    {
        target = _target;
    }
    // private void FixedUpdate()
    // {
    //     if (!target)
    //         return;
    //     Vector2 direction = (target.position - transform.position).normalized;
    //     //normalized makes it only 0 or 1
    //     rb.velocity = direction * projectileSpeed;

    //     //makes the move direction homing
    // }
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<EnemyHealth>().takeDMG(projectileDMG);
        if (destroyOnCollision)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * projectileSpeed;
    }
    private void Update()
    {
        projectileDuration -= Time.deltaTime;
        if (projectileDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
