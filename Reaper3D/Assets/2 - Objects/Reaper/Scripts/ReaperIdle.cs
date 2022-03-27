using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperIdle : ReaperMovement
{
    public ReaperIdle(ReaperController _reaper) : base(_reaper) { }

    public override void Enter() { }

    public override void FrameUpdate()
    {
        base.FrameUpdate();


        // Debug.Log("Idle");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // this.Reaper.SetTargetVelocityHorizontal(0f, this.Reaper.speed * Time.fixedDeltaTime);
    }

    public override void Exit() { }

    public override void CheckState()
    {
        if(!this.Reaper.OnGround) {
            this.Reaper.ChangeState(this.Reaper.FallState);
            return;

        }
        if(this.Reaper.JumpsLeft < this.Reaper.jumpCount && this.Reaper.OnGround)
        {
            this.Reaper.ResetJump();
            return;
        }

        if(this.Reaper.Inputs.Jump && this.Reaper.CanJump)
        {
            Debug.Log("Jump");
            this.Reaper.ChangeState(this.Reaper.JumpState);
            return;
        }
        

        if(this.Reaper.Inputs.Dir.x != 0)
        {
            this.Reaper.ChangeState(this.Reaper.RunState);
            return;
        }


    }
}
