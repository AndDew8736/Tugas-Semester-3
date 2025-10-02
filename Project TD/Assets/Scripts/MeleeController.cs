using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Stats")]
    [SerializeField] private float projectileSpeed = 0f;
    [SerializeField] private int projectileDMG = 1;
    [SerializeField] private float projectileDuration = 1f;
    private Transform target;
    public void setTarget(Transform _target)
    {
        target = _target;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<EnemyHealth>().takeDMG(projectileDMG);
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
