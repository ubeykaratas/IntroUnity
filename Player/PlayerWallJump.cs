using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerWallJump : Base
{
    Player player;
    private Rigidbody2D rb;
    private bool isJumpingOff = false;
    [SerializeField] float jumpOffTime = 0.02f;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        if (isJumpingOff)
        {
            //Cut move
            player.dir = 0;
            //Wait for 0.02 sec
            StartCoroutine(DontMove());
        }
    }

    public override void FixedUpdate()
    {
        DoWallJump();
    }

    private void DoWallJump()
    {
        if ((player.isWallSliding || player.isTouchingWall) &&  player.canJump)
        {
            player.canJump = false;
            player.GetNormalWallJump();
            isJumpingOff = true;
            player.canDoubleJump = true;
        }
    }

    private IEnumerator DontMove()
    {
        yield return new WaitForSeconds(jumpOffTime);
        isJumpingOff = false;
    }

}
