using System;
using UnityEngine;
using Pathfinding;

public class EnemyAttack : MonoBehaviour
{
    [Header("Movement to target")]
    [SerializeField] protected float maxSpeed;
    public Transform target;
    
    protected AIDestinationSetter destinationSetter;
    protected AIPath aiPath;

    public void Stop()
    {
        aiPath.maxSpeed = 0;
    }

    public void Resume()
    {
        aiPath.maxSpeed = maxSpeed;
    }

    public virtual void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        Resume();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        // resume moving
        aiPath.maxSpeed = maxSpeed;
        destinationSetter.target = target;
    }
}