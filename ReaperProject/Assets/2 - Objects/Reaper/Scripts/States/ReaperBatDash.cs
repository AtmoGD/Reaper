using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperBatDash : ReaperState
{
    public ReaperBatDash(ReaperController _reaper) : base(_reaper) { }

    public Vector2 TargetDir { get; set; }
    public float Distance { get; set; }

    public override void Enter()
    {
        TargetDir = Reaper.Inputs.Dir;
    }

    public override void FrameUpdate()
    {
        if(Distance <= 0)
        {
            Reaper.ChangeState(Reaper.IdleState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        Vector2 movement = TargetDir * Reaper.dashSpeed * Time.fixedDeltaTime;
        Vector2 newPosition = Reaper.rb.position + movement;
        Reaper.rb.MovePosition(newPosition);

        Distance -= movement.magnitude;
    }

    public override void Exit()
    {
    }
}
