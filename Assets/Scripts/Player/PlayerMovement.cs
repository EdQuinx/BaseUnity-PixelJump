using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerMovementState
{
    IDLE,
    MOVE,
    JUMP,
    FALL
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float directionX = 0f;
    [SerializeField]
    private LayerMask jumpableGround;
    [SerializeField]
    private float moveSpeed = 7f;
    [SerializeField]
    private float jumpForce = 14f;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(directionX * moveSpeed, rigidbody2D.velocity.y);

        if (Input.GetButtonDown("Jump") && IsCanJump())
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }
        UpdateAnimationState(directionX);
    }

    private void UpdateAnimationState(float directionX)
    {
        ePlayerMovementState movementState;
        // check move or not
        if (directionX != 0f)
        {
            movementState = ePlayerMovementState.MOVE;
            spriteRenderer.flipX = directionX > 0f ? false : true;
        }
        else
        {
            movementState = ePlayerMovementState.IDLE;
        }

        if (rigidbody2D.velocity.y > .1f)
        {
            movementState = ePlayerMovementState.JUMP;
        }
        else if (rigidbody2D.velocity.y < -.1f)
        {
            movementState = ePlayerMovementState.FALL;
        }

        animator.SetInteger("movementState", (int)movementState);
    }

    private bool IsCanJump()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
