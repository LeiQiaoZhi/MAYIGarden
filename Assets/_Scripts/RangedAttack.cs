using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class RangedAttack : EnemyAttack
{
    public float rotateSpeed;
    [Header("Shoot")] public GameObject bulletPrefab;
    public float range;
    public Transform firePoint;
    public float secondsBetweenFire;


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
            var bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}