using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration
    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] Vector2 deathKick = new Vector2(-25.0f, 25.0f);

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D rigidBody;
    Animator animator;
    CapsuleCollider2D bodyCollider2D;
    BoxCollider2D feetCollider2D;
    float gravityScaleAtStart;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        ClimbLadder();
        Jump();
        ChangeDirection();
        Die();
    }

    private void Die()
    {
        if (bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            animator.SetTrigger("IsDying");
            rigidBody.velocity = deathKick;
        }
    }

    private void Run()
    {
        // Between -1 and +1
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(horizontalAxis * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        bool isRunning = HasHorizontalSpeed();

        animator.SetBool("IsRunning", isRunning);
    }

    private void ClimbLadder()
    {
        if (!IsOnLadder())
        {
            animator.SetBool("IsClimbing", false);
            rigidBody.gravityScale = gravityScaleAtStart;

            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, controlThrow * climbSpeed);
        rigidBody.velocity = climbVelocity;
        rigidBody.gravityScale = 0;

        bool isClimbing = HasVerticalSpeed();

        animator.SetBool("IsClimbing", isClimbing);
    }

    private void Jump()
    {
        if (!IsOnSolidGround())
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0.0f, jumpSpeed);
            rigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void ChangeDirection()
    {
        if (HasHorizontalSpeed())
        {
            if (Mathf.Sign(rigidBody.velocity.x) == 1)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    private bool HasHorizontalSpeed()
    {
        return Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
    }

    private bool HasVerticalSpeed()
    {
        return Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
    }

    private bool IsOnSolidGround()
    {
        return feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsOnLadder()
    {
        return feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"));
    }

    private bool IsClimbingLadder()
    {
        return !IsOnSolidGround() && IsOnLadder();
    }
}
