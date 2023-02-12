using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class playerMovement : MonoBehaviour
{
    //[Range(0, .3f)][SerializeField] private float myMovementSmoothing = .05f;
    public Transform scanAreaCentre;
    public SpriteRenderer spriteRenderer;
    public Sprite horizontalSprite;
    public Sprite verticalSprite;
    public Rigidbody2D myRigidbody;

    public List<ParticleSystem> myDusts;

    // isRight means the key board tells to move right, when moving left, isRight = -1
    private int isRight = 1;

    private Animator _animator;

    // similar for isUp
    private int isUp = 1;
    public float attackCentreDist = 1;
    [Range(0, 40)] [SerializeField] public float mySpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        gameObject.name = "Hanson";
    }

    void Update()
    {
        //Get motion value from keys
        float horizontalValue = Input.GetAxisRaw("Horizontal");
        float verticalValue = Input.GetAxisRaw("Vertical");

        // the initial velocity in each frame is zero
        Vector2 currentVelocity = Vector2.zero;
        //if there is horizontal movement
        if (horizontalValue != 0)
        {
            CreateDust();
            //add the horizontal velocity to current velocity
            currentVelocity += mySpeed * Vector2.right * horizontalValue;
            // if the horizontal movement input is in opposite to the direction facing
            if (horizontalValue * isRight < 0)
            {
                //change direction of facing
                // flipDir("Horizontal");
                // now moves to opposite direction
                isRight = -isRight;
            }

            //if the ant sprite is previously vertical
            if (spriteRenderer.sprite != horizontalSprite)
            {
                // changeSprite("Horizontal");
            }
        }
        //else is added as we dont want the ant to move diagonally
        else
        {
            if (verticalValue != 0)
            {
                CreateDust();
                currentVelocity += mySpeed * Vector2.up * verticalValue;
                if (verticalValue * isUp < 0)
                {
                    // flipDir("Vertical");
                    isUp = -isUp;
                }

                if (spriteRenderer.sprite != verticalSprite)
                {
                    // changeSprite("Vertical");
                }
            }
        }

        myRigidbody.velocity = currentVelocity;
        _animator.SetFloat("Horizontal", horizontalValue);
        _animator.SetFloat("Vertical", verticalValue);
        _animator.SetFloat("Speed", currentVelocity.magnitude);
    }

    public IEnumerator AttackAnimate(float distance, float attackAnimateTime, float attackScaleRatio = 1.5f)
    {
        Vector2 attackScale = spriteRenderer.transform.localScale;
        attackScale.x *= attackScaleRatio;
        attackScale.y /= attackScaleRatio;
        Vector2 originalScale = spriteRenderer.transform.localScale;
        spriteRenderer.transform.Translate(distance, 0, 0);
        spriteRenderer.transform.localScale = attackScale;
        yield return new WaitForSeconds(attackAnimateTime);
        spriteRenderer.transform.Translate(-distance, 0, 0);
        spriteRenderer.transform.localScale = originalScale;
    }

    public void freezePos()
    {
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void UnfreezePos()
    {
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void CreateDust()
    {
        //Debug.Log("Dust Created!");
        foreach (var dust in myDusts)
        {
            dust.Play();
        }
    }

    private void flipDir(string direction)
    {
        Vector2 localScale = gameObject.transform.localScale;
        if (direction == "Horizontal")
        {
            localScale.x = -localScale.x;
        }

        if (direction == "Vertical")
        {
            localScale.y = -localScale.y;
        }

        gameObject.transform.localScale = localScale;
    }

    //!!! I added change scanAreaCentre in this function
    private void changeSprite(string direction)
    {
        Vector2 localScale = gameObject.transform.localScale; //hard code to avoided inverted sprite when moving.
        if (direction == "Horizontal")
        {
            //keep the vertical scale to positive when moving horizontally
            //if (localScale.y < 0)
            //{
            //   localScale.y = Mathf.Abs(localScale.y);
            //   gameObject.transform.localScale = localScale;
            //}

            // Change direction of attack centre when changing to horizontal motion
            //I multiply by localScale.x as it accounts for left/right.
            scanAreaCentre.position = gameObject.transform.position + attackCentreDist * Vector3.right * localScale.x;
            spriteRenderer.sprite = horizontalSprite;
        }

        if (direction == "Vertical")
        {
            // Change direction of attack centre
            scanAreaCentre.position = gameObject.transform.position + attackCentreDist * Vector3.up * localScale.y;
            spriteRenderer.sprite = verticalSprite;
        }
    }
}