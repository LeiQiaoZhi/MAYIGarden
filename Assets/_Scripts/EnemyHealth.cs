using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [Tooltip("Seconds to blink if being attacked")]
    public float blinkSecond = 0.1f;
    public float freezeSecond = 0.1f;
    [SerializeField] private GameEvent enemyDeathEvent;
    private Rigidbody2D _rigidbody2D;


    // ReSharper disable Unity.PerformanceAnalysis
    public override void Start()
    {
        base.Start();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void ChangeHealth(int change, GameObject from)
    {
        // attacked feedback
        if (change < 0)
        {
            StartCoroutine(Attacked());
            StartCoroutine(Freeze());
        }
        base.ChangeHealth(change, from);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Freeze()
    {
        Debug.LogWarning("Enemy is frozen");
        // EnemyAttack enemyAttack = GetComponent<EnemyAttack>();
        // enemyAttack.Stop();
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(freezeSecond);

        Debug.LogWarning("Enemy Resume");
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        // enemyAttack.Resume();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Attacked()
    {
        SpriteRenderer sp = GetComponentInChildren<SpriteRenderer>();
        Color temp = sp.color;
        temp.a = 0f;
        sp.color = temp;

        yield return new WaitForSeconds(blinkSecond);
        temp.a = 1f;
        sp.color = temp;
    }

    protected override void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        enemyDeathEvent.Raise();
        Destroy(gameObject);
    }
}