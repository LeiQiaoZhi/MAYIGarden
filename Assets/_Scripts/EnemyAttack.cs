using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAttack : MonoBehaviour
{
    public float stopThreshold = 0.2f;

    public float secondsBetweenAttacks = 1f;
    public int attackDamage = 1;

    private AIDestinationSetter _destinationSetter;

    // Start is called before the first frame update
    void Start()
    {
        _destinationSetter = GetComponent<AIDestinationSetter>();

        // find root and assign it as target
        _destinationSetter.target = FindObjectOfType<Root>().transform;
    }

    // Update is called once per frame
    private bool _attacking;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_attacking && col.CompareTag("Root"))
        {
            Debug.LogWarning("Detect Root");
            // stop and attack
            _attacking = true;
            StartCoroutine(AttackCoroutine(_destinationSetter.target));
            _destinationSetter.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_attacking && other.CompareTag("Root"))
        {
            _attacking = false;
            _destinationSetter.enabled = true;
        }
    }

    IEnumerator AttackCoroutine(Transform target)
    {
        while (_attacking)
        {
            Attack(target);
            yield return new WaitForSeconds(secondsBetweenAttacks);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Attack(Transform target)
    {
        Debug.Log($"Attacking {target.name}");
        Health health = target.GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError($"{target.name} doesn't have Health");
        }
        else
        {
            health.ChangeHealth(-attackDamage, gameObject);
        }
    }
}