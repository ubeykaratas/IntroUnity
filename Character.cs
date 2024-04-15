using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public abstract class Character : Base
{

    protected Rigidbody2D rb;
    protected Animator animator;

    [Header("Movement")]
    [SerializeField] public float speed = 1.0f;
    [ReadOnly] public float dir;

    [ReadOnly] public bool facingLeft = false;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] protected float _jumpTime;
    [SerializeField] protected Transform groundCheckPoint;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float checkRadius;
    [SerializeField] public bool isGrounded;
    //protected float jumpTimeCounter;

    [Header("Double Jump")]
    [SerializeField] protected float doubleJumpForce = 2.0f;
    [ReadOnly] public bool canJump = false;
    [ReadOnly] public bool canDoubleJump = false;

    [Header("Wall Slide")]
    [SerializeField] protected float wallSlideSpeed;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Vector2 wallCheckSize;
    [ReadOnly] public bool isTouchingWall;
    [ReadOnly] public bool isWallSliding;

    [Header("Wall Jump")]
    [SerializeField] Vector2 wallJumpForce;
    [ReadOnly] public float wallJumpDir = -1f;

    [Header("Dash")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashTime = 0.2f;
    [SerializeField] public float dashCooldown = 1f;
    [ReadOnly] public bool isDashing = false;
    [ReadOnly] public bool canDash = true;



    #region monos

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public override void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void Update()
    {
        //handle input
        worldCheck();
    }

    public override void FixedUpdate()
    {
        //handle physics
        HandleMovement();
    }

    #endregion

    #region Mechanics
    protected void Move()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }
        else if (!isGrounded && !isTouchingWall && dir != 0)
        {
            rb.AddForce(new Vector2(dir * speed, 0), ForceMode2D.Impulse);
            if (Mathf.Abs(rb.velocity.x) > dir)
            {
                rb.velocity = new Vector2(dir * speed, rb.velocity.y);
            }
        }

    }

    protected void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    protected void DoubleJump()
    {
        //rb.AddForce(new Vector2(0, jumpForce * doubleJumpForce), ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * doubleJumpForce);
    }

    protected void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);
    }

    protected void WallJump()
    {
        rb.AddForce(new Vector2(wallJumpDir * wallJumpForce.x, wallJumpForce.y), ForceMode2D.Impulse);
    }

    #endregion

    #region SubMechanics
    protected abstract void HandleJump();
    protected virtual void HandleMovement()
    {
        Move();
    }

    protected void Flip(float moveDir)
    {
        if ( (facingLeft && moveDir > 0 || !facingLeft && moveDir < 0) && !isWallSliding)
        {
            facingLeft = !facingLeft;
            wallJumpDir *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void worldCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, whatIsGround);
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }

    #endregion

    #region Visual
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckPoint.position, checkRadius);
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
    #endregion

    #region Getter-Setter

    public float jumpForce { get; set; }
    public float jumpTime { get; set; }

    #endregion

}
