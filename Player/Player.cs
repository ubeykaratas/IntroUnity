using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Player : Character
{
    [Header("Player Speed")]
    [SerializeField] private float runSpeed = 2.0f;
    [SerializeField] private float walkSpeed = 1.0f;

    [Header("Player Jump")]
    [SerializeField] private float jumpTimeCounter;

    [Header("Den")]
    [SerializeField] private SlowMotion slowMotion;

    [SerializeField] private float slo;

    private bool cJump;


    public override void Start()
    {
        base.Start();
        slo = Time.fixedDeltaTime;
        speed = runSpeed;
    }

    public override void Update()
    {
        base.Update();
        inputs();
        checkPress();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        HandleJump();
    }

    protected void inputs()
    {
        dir = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) canJump = true;
    }

    #region Movement
    protected override void HandleMovement()
    {
        base.HandleMovement();
        Flip(dir);
    }
    #endregion

    #region Jump
    protected override void HandleJump()
    {
        if(isGrounded && canJump)
        {
            canJump = false;
            jumpTimeCounter = jumpTime;
        }
        if(Input.GetKey(KeyCode.Space) && (jumpTimeCounter > 0) && !isWallSliding)
        {
            canJump = false;
            Jump();
            jumpTimeCounter -= Time.deltaTime;
        }
        if (cJump)
        {
            cJump = false;
            jumpTimeCounter = 0;
        }


    }
    #endregion

    public void GetDoubleJump()
    {
        DoubleJump();
    }

    public void GetWallSlide()
    {
        WallSlide();
    }

    public void GetNormalWallJump()
    {
        WallJump();
    }

    private void checkPress()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            cJump = true;
        }
    }

    


}
