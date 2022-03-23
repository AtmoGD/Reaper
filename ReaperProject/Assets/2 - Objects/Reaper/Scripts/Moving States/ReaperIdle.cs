using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperIdle : ReaperState
{
    public ReaperIdle(ReaperController _reaper) : base(_reaper) { }

    public override void Enter() { }

    public override void FrameUpdate()
    {
        if (this.Reaper.Inputs.Jump && this.Reaper.CanJump)
        {
            this.Reaper.ChangeState(this.Reaper.JumpingState);
            return;
        }

        if (!this.Reaper.OnGround || this.Reaper.Inputs.Dir != Vector2.zero)
        {
            this.Reaper.ChangeState(this.Reaper.MovingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        this.Reaper.Move(-this.Reaper.rb.velocity, 1f);
    }

    public override void Exit() { }
}
