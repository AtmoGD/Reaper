using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperJump : ReaperMovement
{
    public ReaperJump(ReaperController _reaper) : base(_reaper) { }

    // private float timeSinceActive;
    // private float jumpTimeMin;

    public override void Enter() {
        base.Enter();

        // timeSinceActive = 0f;

        // jumpTimeMin = this.Reaper.jumpTimeMin;

        this.Reaper.UseJump();
     }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // timeSinceActive += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        this.Reaper.MoveVertical(this.Reaper.jumpSpeed * Time.fixedDeltaTime);
    }

    public override void Exit() { 
        InputController.Instance.UseJump();
        base.Exit();
    }

    public override void CheckState()
    {
        if(!this.Reaper.Inputs.Jump && timeSinceActive > this.Reaper.jumpTimeMin)
        {
            this.Reaper.ChangeState(this.Reaper.FallState);
            return;
        }

        if(timeSinceActive >= this.Reaper.jumpTime)
        {
            this.Reaper.ChangeState(this.Reaper.FallState);
            return;
        }

        if (this.Reaper.OnGround && timeSinceActive > this.Reaper.jumpTimeMin)
        {
            // THIS IS GONIG TO BE THE LAND STATE
            this.Reaper.ChangeState(this.Reaper.RunState);
            return;
        }


    }
}
