using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperIdle : ReaperState
{
    public ReaperIdle(ReaperController _reaper) : base(_reaper) { }

    public override void Enter() { }

    public override void FrameUpdate()
    {
        if(!Reaper.OnGround)
        {
            Reaper.ChangeState(Reaper.FallingState);
            return;
        }

        if(Reaper.Inputs.Dir != Vector2.zero)
        {
            Reaper.ChangeState(Reaper.MovingState);
            return;
        }

        if(Reaper.Inputs.Jump && Reaper.CanJump)
        {
            Reaper.ChangeState(Reaper.JumpingState);
            return;
        }

        if(Reaper.Inputs.BatDash && Reaper.Inputs.Dir.magnitude > 0)
        {
            Reaper.ChangeState(Reaper.BatDashCastState);
            return;
        }

    }

    public override void PhysicsUpdate() { }

    public override void Exit() { }
}
