using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Nutrient : MonoBehaviour
{
    // [SerializeField] private float spawnHeight=12;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    public virtual void Start()
    {
        
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

    public void SpawnFromSky(Vector3 targetPos,float fallSpeed = 2)
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        transform.position = new Vector3(targetPos.x, 12, transform.position.z);
        StartCoroutine(FallFromSky(targetPos, fallSpeed));
    }
    IEnumerator FallFromSky(Vector3 targetPos, float fallSpeed)
    {
        _collider.enabled = false;
        _spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        Debug.LogWarning($"{targetPos},{fallSpeed}");
        while (-(targetPos.y-transform.position.y) > 0.1f)
        {
            transform.position +=  Vector3.down * (fallSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        _collider.enabled = true;
        _spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(Vector3.up *12,new Vector3(20,0.1f,1));
    }
}