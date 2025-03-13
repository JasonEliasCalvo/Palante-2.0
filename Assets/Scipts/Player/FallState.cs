using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : BaseState
{
    public FallState(Player_Movement parameterController) : base(parameterController) { }
    public override void EnterState()
    {
        Debug.Log("Entro a caer");
        //controller.anim.CrossFade("Fall", 0.2f);
    }

    public override void FixedUpdateState()
    {
        controller.rigid.velocity = new Vector3(controller.horizontal * controller.speedMovement, controller.rigid.velocity.y, controller.rigid.velocity.z);
    }

    public override void UpdateState()
    {
        if (controller.isGrounded)
        {
            if (controller.horizontal == 0 && controller.vertical == 0)
            {
                ExitState(controller._idle);
            }
            else ExitState(controller._walk);
        }
    }

    public override void ExitState(BaseState nextState)
    {
        controller.ChangeState(nextState);
    }
}
