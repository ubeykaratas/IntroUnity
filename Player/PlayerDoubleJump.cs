using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerDoubleJump : Base
{
    Player player;
    private bool handleDoubleJump;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public override void Update()
    {
        CheckDoubleJump();
    }

    public override void FixedUpdate()
    {
        DoDoubleJump();
    }

    private void CheckDoubleJump()
    {
        if (player.isGrounded)
        {
            player.canDoubleJump = true;
        }
        if (player.canJump && !player.isGrounded && player.canDoubleJump && !player.isTouchingWall)
        {
            player.canDoubleJump = false;
            player.canJump = false;
            handleDoubleJump = true;
        }
    }

    private void DoDoubleJump()
    {
        if (handleDoubleJump)
        {
            handleDoubleJump = false;
            player.GetDoubleJump();
        }
    }

}