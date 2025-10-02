using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;
using System;
using Unity.Mathematics;

public class TowerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firingPoint;
    [Header("Stats")]
    [SerializeField] private float targetingRange = 8f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float shotsPerSecond = 1f;
    private Transform target;
    private float timeUntilShot;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            findTarget();
            return;
        }
        else
        {
            timeUntilShot += Time.deltaTime;
            if (timeUntilShot >= 1f / shotsPerSecond)
            {
                shoot();
                timeUntilShot = 0f;
            }
        }
        rotateToTarget();
        if (!checkInRange())
        {
            target = null;
        }
    }
    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
        (Vector2)transform.position, 0f, enemyMask);
        //does a raycast to determine the target
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
        //if the raycast hits an enemy, sets it as the target
    }
    private void rotateToTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x)
        * Mathf.Rad2Deg;
        //gets the angle by getting the degree and then converting it to rad
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //quaternion to make targetrotation with the vectors of 0,0, angle
        //makes the transform rotation snap to a target slowly instead of instantly
    }
    private bool checkInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
        //makes the preview range circle
    }
    private void shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, transform.rotation);
        ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
        projectileScript.setTarget(target);
        //spawns in a gameobject projectile with firing position and quaternion for the angle
        //takes parts of the projectile controller script
        //sets the target of the tower to the target of the projectile controller
    }
}
