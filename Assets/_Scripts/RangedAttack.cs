using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class RangedAttack : EnemyAttack
{
    [Header("Shoot")]
    public GameObject bulletPrefab;
    public float range;
    public Transform firePoint;
    public float secondsBetweenFire;
    [Header("Movement to target")]
    public Transform target;
    public float rotateSpeed;


    public override void Start()
    {
        base.Start();
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void Shoot()
    {
        if (target)
        {
            var bullet = Instantiate(bulletPrefab,firePoint.position,transform.rotation);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        // resume moving
        aiPath.maxSpeed = maxSpeed;
        destinationSetter.target = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
