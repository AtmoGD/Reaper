using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperFall : ReaperMovement
{
    public ReaperFall(ReaperController _reaper) : base(_reaper) { }

    public override void Enter() { 
        base.Enter();
        // Debug.Log("Fall");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Debug.Log("Idle");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        this.Reaper.MoveVertical(-this.Reaper.fallSpeed * Time.fixedDeltaTime);
    }

    public override void Exit() {
        base.Exit();
     }

    public override void CheckState()
    {
        base.CheckState();

        if (this.Reaper.OnGround)
        {
            this.Reaper.ResetVertical();

            if (this.Reaper.Inputs.Dir.x != 0)
            {
                this.Reaper.ChangeState(this.Reaper.RunState);
                return;
            }

            this.Reaper.ChangeState(this.Reaper.IdleState);
            return;

        }
    }
}
