using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperJump : ReaperMovement
{
    public ReaperJump(ReaperController _reaper) : base(_reaper) { }

    private float jumpTime;
    private float jumpTimeMin;

    public override void Enter() {
        

        jumpTime = 0f;

        jumpTimeMin = this.Reaper.jumpTimeMin;

        this.Reaper.UseJump();
     }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Debug.Log("Jump");
        jumpTime += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        this.Reaper.MoveVertical(this.Reaper.jumpSpeed * Time.fixedDeltaTime);
    }

    public override void Exit() { 
        InputController.Instance.UseJump();
    }

    public override void CheckState()
    {
        if(!this.Reaper.Inputs.Jump && jumpTime > jumpTimeMin)
        {
            this.Reaper.ChangeState(this.Reaper.FallState);
            return;
        }

        if (this.Reaper.OnGround && jumpTime > jumpTimeMin)
        {
            Debug.Log("Idle");
            // THIS IS GONIG TO BE THE LAND STATE
            this.Reaper.ChangeState(this.Reaper.RunState);
            return;
        }

        if(jumpTime >= this.Reaper.jumpTime)
        {
            Debug.Log("jumpTime");
            this.Reaper.ChangeState(this.Reaper.FallState);
            return;
        }

    }
}
