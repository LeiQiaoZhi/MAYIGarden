using System;
using UnityEngine;
using Pathfinding;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float maxSpeed;
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
}