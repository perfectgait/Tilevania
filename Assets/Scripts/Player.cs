using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration
    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D rigidBody;
    Animator animator;
    Collider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        ChangeDirection();
        ClimbLadder();
    }

    private void Run()
    {
        // Between -1 and +1
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        bool isRunning = HasHorizontalSpeed();

        animator.SetBool("IsRunning", isRunning);
    }

    private void ClimbLadder()
    {
        if (!IsOnLadder())
        {
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, controlThrow * climbSpeed);
        rigidBody.velocity = climbVelocity;

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
        return collider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsOnLadder()
    {
        return collider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"));
    }
}
