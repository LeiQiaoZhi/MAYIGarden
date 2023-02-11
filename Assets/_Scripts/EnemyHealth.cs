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
        EnemyAttack enemyAttack = GetComponent<EnemyAttack>();

        enemyAttack.Stop();
        yield return new WaitForSeconds(freezeSecond);

        enemyAttack.Resume();
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