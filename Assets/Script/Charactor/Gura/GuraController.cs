using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraController : PlayerController
{
    private Animator amt;
    private bool isMove = false;
    private GuraSkill gs;
    
    protected override void Start()
    {
        base.Start();
        amt = GetComponent<Animator>();
        gs = GetComponent<GuraSkill>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        AnimatorControl();
    }

    protected override void CheckInput()
    {
        base.CheckInput();
        if(base._Move != Vector2.zero)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }
    }


    private void AnimatorControl()
    {
        amt.SetBool("isMove", isMove);
        amt.SetBool("isDive", gs.IsDive);
    }
}
