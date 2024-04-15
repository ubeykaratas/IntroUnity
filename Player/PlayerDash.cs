using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerDash : Base
{
    Player player;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    public override void Update()
    {
        if (player.isDashing) player.dir = -player.wallJumpDir;
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canDash) StartCoroutine(StartDash());
    }

    private IEnumerator StartDash()
    {
        float originalSpeed = player.speed;

        player.canDash = false;
        player.isDashing = true;

        rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionY;
        player.speed = player.dashSpeed;

        yield return new WaitForSeconds(player.dashTime);
        player.speed = originalSpeed;
        rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(-player.wallJumpDir * player.speed, rb.velocity.y);
        player.isDashing = false;

        yield return new WaitForSeconds(player.dashCooldown);
        player.canDash = true;
    }

    


}
