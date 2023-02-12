using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class playerAttack : MonoBehaviour
{
    public playerMovement myMovement;

    // public float scanAreaRadius = 10f;
    [SerializeField] private Vector2 scanAreaSize;
    public Transform scanAreaCentre; // the centre position of plyaer's attack scanning region
    public float secondsBetweenAttacks = 1f;
    private float nextAttackTime = 0f;
    public LayerMask targetLayers;
    private Transform _target;
    public int playerAttackDamage = 1;
    public float attackDist = 3f;
    public float attackAnimateTime = 0.5f;
    public float attackAnimateScale = 1.5f;
    public KeyCode attackKey = KeyCode.J;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackKey) && Time.time >= nextAttackTime)
        {
            // When the player is attacking

            // Added movement animation 
            // StartCoroutine(myMovement.AttackAnimate(attackDist,attackAnimateTime,attackAnimateScale));
            animator.SetTrigger("Attack");


            nextAttackTime = Time.time + secondsBetweenAttacks;
            //1. find all enemies in the radius
            Debug.Log("Player trying to attack");
            Collider2D[] hits = Physics2D.OverlapBoxAll(scanAreaCentre.position, scanAreaSize, 0, targetLayers);
            foreach (Collider2D hit in hits)
            {
                Debug.LogWarning($"Player Attack Target Acquired -- {hit.name}");
                _target = hit.transform;
                Attack(_target);
            }

            if (hits.Length > 0)
            {
                AudioManager.Instance.PlaySound("PlankHit");
            }
            else
            {
                AudioManager.Instance.PlaySound("VeryLight");
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
        // Gizmos.DrawWireSphere(scanAreaCentre.position, scanAreaRadius);
        Gizmos.DrawWireCube(scanAreaCentre.position, scanAreaSize);
    }
}