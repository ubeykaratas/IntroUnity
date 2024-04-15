using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : Base
{
    Player player;

    public float slowTimeScale = 0.1f;
    private float fixedDeltaTime;
    private float slowDownTime = 2.0f;
    bool canSlowTime = true;


    private void Awake()
    {
        player = GetComponent<Player>();
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        GetSlowMotion();
    }

    public void DoSlowTime()
    {
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = .02f * Time.timeScale;
    }

    public void cutSlowTime()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = .02f * Time.timeScale;
    }

    private void GetSlowMotion()
    {

        if (Input.GetKeyDown("f") && canSlowTime)
        {
            player.jumpForce = player.jumpForce * slowTimeScale;
            DoSlowTime();
            canSlowTime = false;
        }

        if (Input.GetKeyDown("g") && !canSlowTime)
        {
            player.jumpForce = player.jumpForce / slowTimeScale;
            cutSlowTime();
            canSlowTime = true;
        }

    }

}
