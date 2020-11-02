using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidBody.velocity.x)), 1.0f);
    }
}
