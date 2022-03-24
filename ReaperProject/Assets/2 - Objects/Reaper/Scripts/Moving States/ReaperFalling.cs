using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperFalling : ReaperMoving
{
    public ReaperFalling(ReaperController _reaper) : base(_reaper) { }

    public override void Enter()
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
        
        if (this.Reaper.OnGround)
        {
            this.Reaper.ChangeState(this.Reaper.MovingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        this.Reaper.ApplyGravity();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
