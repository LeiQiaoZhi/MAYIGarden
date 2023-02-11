using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [Header("Shoot")]
    public GameObject bulletPrefab;
    public float range;
    public Transform firePoint;
    public float secondsBetweenFire;
    [Header("Movement to target")]
    public Transform target;
    public float rotateSpeed;
    [SerializeField] private float maxSpeed;
    
    private AIDestinationSetter _aiDestinationSetter;
    private AIPath _aiPath;

    private void Start()
    {
        _aiPath = GetComponent<AIPath>();
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
        _aiPath.maxSpeed = maxSpeed;
    }

    public void Shoot()
    {
        if (target)
        {
            var bullet = Instantiate(bulletPrefab,firePoint.position,transform.rotation);
        }
    }

    public void Stop()
    {
        _aiPath.maxSpeed = 0;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        // resume moving
        _aiPath.maxSpeed = maxSpeed;
        _aiDestinationSetter.target = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
