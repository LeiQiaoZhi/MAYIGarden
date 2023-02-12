using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attack any nearest attackable thing
/// </summary>
public class AggressiveEnemy : EnemyAttack
{
    [Header("Find Target")] public float findRadius;
    public int damage = 1;
    public LayerMask attackableThings;
    [SerializeField] private Transform scannAreaCenter;
    [SerializeField] private float threshold = 2f;
    public float secondsBetweenAttack;
    EnemyAttackCollider _attackCollider;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _attackCollider = GetComponentInChildren<EnemyAttackCollider>();
        _animator = GetComponent<Animator>();
        Transform rootTrans = FindObjectOfType<Root>().transform;
        SetTarget(rootTrans);
    }

    private float _fireTime = 0;
    private Animator _animator;

    private void FixedUpdate()
    {
        FindTarget();
        if (Vector2.Distance(target.position, transform.position) < threshold)
        {
            // in range
            Stop();
            if (Time.time > _fireTime)
            {
                Debug.LogWarning($"Attacking {target.name}");
                _animator.SetTrigger("Attack");
                // collision detection is done at Sprite object
                _fireTime = Time.time + secondsBetweenAttack;
            }
        }
        else
        {
            Resume();
        }
    }

    public void TurnOnAttackCollider()
    {
        _attackCollider.TurnOn(attackableThings, damage);
    }

    public void TurnOffAttackCollider()
    {
        _attackCollider.TurnOff();
    }

    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(scannAreaCenter.position, findRadius, attackableThings);
        if (hits.Length == 0)
        {
            return;
        }

        var minDistance = -1f;
        var minIndex = -1;

        for (int i = 0; i < hits.Length; i++)
        {
            var distance = Vector2.Distance(transform.position, hits[i].transform.position);
            if (minDistance < 0 || distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        SetTarget(hits[minIndex].transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(scannAreaCenter.position, findRadius);
        Gizmos.DrawWireSphere(transform.position, threshold);
    }
}