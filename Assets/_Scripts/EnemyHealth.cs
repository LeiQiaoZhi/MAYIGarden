using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [Tooltip("Seconds to blink if being attacked")]
    public float blinkSecond = 0.1f;
    public float freezeSecond = 0.1f;


    // ReSharper disable Unity.PerformanceAnalysis
    public override void ChangeHealth(int change, GameObject from)
    {
        base.ChangeHealth(change, from);
        // attacked feedback
        if (change < 0)
        {
            StartCoroutine(Attacked());
            StartCoroutine(Freeze());
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Freeze()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 vel = rb.velocity;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(freezeSecond);

        rb.velocity = vel;
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
        Destroy(gameObject);
    }
}