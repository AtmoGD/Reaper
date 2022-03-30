using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperRun : ReaperMovement
{
    public ReaperRun(ReaperController _reaper) : base(_reaper) { }

    public override void Enter() {
        base.Enter();
     }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Debug.Log("Run");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit() {
        base.Exit();
     }

    public override void CheckState()
    {
        base.CheckState();

        if (Mathf.Abs(this.Reaper.rb.velocity.x) <= 0.1f && Mathf.Abs(this.Reaper.Inputs.Dir.x) <= 0.1f)
        {
            this.Reaper.ChangeState(this.Reaper.IdleState);
            return;
        }
        
        if (!this.Reaper.OnGround)
        {
            this.Reaper.ChangeState(this.Reaper.FallState);
            return;
        }
    }
}
