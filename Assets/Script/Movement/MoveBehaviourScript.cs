using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviourScript : MonoBehaviour
{
    [Header("Movement Variables")]
    public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    public Animator animator;
    private bool isFacingRight = true;
    public bool frogIsAlive = true;
    public LogicScript logic;

    [Header("Wall Movement")]
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallSlidingSpeed = 2f;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(4f, 16f);

    [Header("Components")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("Knockback")]
    public float knockbackForce = 10.0f; 
    public float knockbackDuration = 0.5f; 
    public Vector2 knockbackDirection;
    public bool isKnockedBack = false;

    [Header("Special Actions")]
    private bool isLaserActive = false;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource jumpSound;
    
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic"). GetComponent<LogicScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (!isKnockedBack)
        {
            if (!isLaserActive)
            {
                Movement();
                WallSlide();
                WallJump();
            }
            Jump();
        }
        else  
        {
            horizontal = 0;
        }

        if (!isWallJumping)
        {
            Flip();
        }

    }

    public void Movement()
    {
        if (GetComponent <Health>(). currentHealth > 0 && frogIsAlive)
        {
            if (!isWallJumping && !isLaserActive) 
            {
                horizontal = Input.GetAxisRaw("Horizontal");
                animator.SetFloat("Speed", Mathf.Abs(horizontal));

                Vector2 movement = new Vector2(horizontal * speed, rb.velocity.y);
                rb.velocity = movement;
            }
        }
        else 
        {
            StopMovement();
        }
    }

    public void StopMovement()
    {
        frogIsAlive = false;
        animator.SetFloat("Speed", 0);
        speed = 0;
        rb.velocity = Vector2.zero;
    }

    public void ResumeMovement()
    {
        frogIsAlive = true;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        speed = 8f; 
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && frogIsAlive)
        {
            if (IsGrounded())
            {
                jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                //Debug.Log("Jump: " + gameObject.name);
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f && frogIsAlive)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }
    
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump() 
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector2 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isKnockedBack && frogIsAlive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

                ApplyKnockback(knockbackDirection);

                isKnockedBack = true;
                StartCoroutine(ApplyKnockback());
            }
        }
    }

    private void ApplyKnockback(Vector2 direction)
    {
        rb.velocity = Vector2.zero; 
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    private IEnumerator ApplyKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false; // Allow movement and jumping again
        rb.velocity = Vector2.zero;
    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }

    public void SetLaserActive(bool isActive)
    {
        isLaserActive = isActive;
    }

}
   