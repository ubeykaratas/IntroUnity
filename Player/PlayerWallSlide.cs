using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlide : Base
{
    Player player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    public override void FixedUpdate()
    {
        DoWallSlide();
    }

    /// <summary>
    /// Checks if the player is attached to any wall. If so, allows the player to slide through walls 
    /// </summary>
    private void DoWallSlide()
    {
        if (player.isTouchingWall && !player.isGrounded && rb.velocity.y < 0)
        {
            player.isWallSliding = true;

            //perform the wall slide
            player.GetWallSlide();
        }
        else
        {
            player.isWallSliding = false;
            player.canJump = false;
        }
    }
    
}
