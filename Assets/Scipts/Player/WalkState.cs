using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState
{
    public WalkState(Player_Movement parameterController) : base(parameterController) { }

    public override void EnterState()
    {
        Debug.Log("Entro a caminar");
    }

    public override void FixedUpdateState()
    {
        //Vector3 moveDirection = controller.transform.forward * controller.vertical * controller.speedMovement;
        Vector3 moveDirection = (controller.transform.forward * controller.vertical + controller.transform.right * controller.horizontal).normalized;
        controller.rigid.velocity = moveDirection * controller.speedMovement;
    }

    public override void UpdateState()
    {
        if (controller.isGrounded)
        {
            if (controller.vertical == 0)
            {
                ExitState(controller._idle);
            }
            else if (Input.GetKeyDown(controller.jumpKey))
            {
                ExitState(controller._jump);
            }
        }
        else
        {
            if (controller.rigid.velocity.y <= 0)
            {
                ExitState(controller._fall);
            }
        }
    }

    public override void ExitState(BaseState nextState)
    {
        controller.ChangeState(nextState);
    }
}
