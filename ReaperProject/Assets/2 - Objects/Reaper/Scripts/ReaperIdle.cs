using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperIdle : ReaperState
{
    public ReaperIdle(ReaperController _reaper) : base(_reaper) { }

    public override void Enter(float useTimeSinceStart = 0f) {
        base.Enter(0f);
     }

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
        this.Reaper.rb.velocity = Vector2.Lerp(this.Reaper.rb.velocity, Vector2.down, this.Reaper.lerpMultiplier * Time.deltaTime);
    }

    public override void Exit() { }
}
