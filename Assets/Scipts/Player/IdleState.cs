using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player_Movement parameterController) : base(parameterController) { }

    public override void EnterState()
    {
        Debug.Log("Entro a idle");
    }

    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {
        if (controller.isGrounded)
        {
            if (controller.vertical != 0 || controller.horizontal != 0)
            {
                ExitState(controller._walk);
            }
            else if (Input.GetKeyDown(controller.jumpKey))
            {
                ExitState(controller._jump);
            }
        }
        else if (controller.rigid.velocity.y <= 0)
        {
            ExitState(controller._fall);
        }
    }

    public override void ExitState(BaseState nextState)
    {
        controller.ChangeState(nextState);
    }
}
