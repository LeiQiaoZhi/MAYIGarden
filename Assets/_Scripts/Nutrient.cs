using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Nutrient : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private float spawnHeight=12;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    public virtual void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        SpawnFromSky(Vector3.zero);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            OnTriggerWithPlayer(col.gameObject);
            Destroy(gameObject);
        }
    }

    public abstract void OnTriggerWithPlayer(GameObject player);

    public void SpawnFromSky(Vector3 targetPos)
    {
        transform.position = new Vector3( targetPos.x, spawnHeight, transform.position.z);
        StartCoroutine(FallFromSky(targetPos));
    }
    IEnumerator FallFromSky(Vector3 targetPos)
    {
        _collider.enabled = false;
        _spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        while (Vector2.Distance(targetPos, transform.position) > 0.1f)
        {
            var direction = (targetPos - transform.position).normalized;
            transform.position += direction * (fallSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        _collider.enabled = true;
        _spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(Vector3.up *spawnHeight,new Vector3(20,0.1f,1));
    }
}