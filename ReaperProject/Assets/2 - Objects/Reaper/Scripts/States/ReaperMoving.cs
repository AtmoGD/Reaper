using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperMoving : ReaperState
{
    public ReaperMoving(ReaperController _reaper) : base(_reaper) { }

    public override void Enter() { }

    public override void FrameUpdate()
    {

        if(!Reaper.OnGround)
        {
            Reaper.ChangeState(Reaper.FallingState);
            return;
        }

        if (Reaper.Inputs.Dir.magnitude <= 0.2f)
        {
            Reaper.ChangeState(Reaper.IdleState);
            return;
        }

        if (Reaper.Inputs.Jump)
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

    public override void PhysicsUpdate()
    {
        Vector2 newPosition = Reaper.rb.position;
        newPosition.x += Reaper.Inputs.Dir.x * Reaper.speed * Time.fixedDeltaTime;

        Reaper.rb.MovePosition(newPosition);
    }

    public override void Exit() { }
}
