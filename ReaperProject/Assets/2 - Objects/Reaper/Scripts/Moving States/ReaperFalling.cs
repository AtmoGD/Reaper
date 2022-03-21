using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperFalling : ReaperMoving
{
    public ReaperFalling(ReaperController _reaper) : base(_reaper) { }

    public override void Enter(float useTimeSinceStart = 0f)
    {
        base.Enter();
    }

    public override void FrameUpdate()
    {
        if (this.Reaper.Inputs.Jump && this.Reaper.CanJump)
        {
            this.Reaper.ChangeState(this.Reaper.JumpingState);
            return;
        }
        
        if (this.Reaper.OnGround && this.TimeSinceStart > 0.1f)
        {
            this.Reaper.ChangeState(this.Reaper.MovingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        // this.MoveHorizontal();

        float verticalMultiplier = this.Reaper.fallCurve.Evaluate(this.TimeSinceStart);

        this.MoveVertical(-this.Reaper.fallSpeed * verticalMultiplier);

        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
