using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class playerAttack : MonoBehaviour
{

    public float scanAreaRadius = 10f;
    public Transform scanAreaCentre; // the centre position of plyaer's attack scanning region
    public float secondsBetweenAttacks = 1f;
    private float nextAttackTime = 0f;
    public LayerMask targetLayers;
    private Transform _target;
    public int playerAttackDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextAttackTime)
        {   
            nextAttackTime= Time.time + secondsBetweenAttacks;
            //1. find all enemies in the radius
            Debug.Log("Player trying to attack");
            Collider2D[] hits = Physics2D.OverlapCircleAll(scanAreaCentre.position, scanAreaRadius, targetLayers);
            foreach (Collider2D hit in hits)
            {
                Debug.LogWarning($"Player Attack Target Acquired -- {hit.name}");
                _target = hit.transform;
                Attack(_target);
            }
        }
    }


    // Detect if there is any enemy within the detection radius
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
            health.ChangeHealth(-playerAttackDamage, gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(scanAreaCentre.position, scanAreaRadius);
    }
}
