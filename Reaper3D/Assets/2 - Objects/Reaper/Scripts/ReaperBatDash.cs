using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperBatDash : ReaperState
{
    public ReaperBatDash(ReaperController _reaper) : base(_reaper) { }

    private float timeActive;

    private Vector2 dashDirection;
    // private float jumpTimeMin;

    public override void Enter() {
        

        timeActive = 0f;

        dashDirection = this.Reaper.Inputs.Dir;

        InputController.Instance.UseBats();

        // jumpTimeMin = this.Reaper.jumpTimeMin;

        // this.Reaper.UseJump();
     }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Debug.Log("Jump");
        timeActive += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        this.Reaper.SetTargetVelocityHorizontal(dashDirection.x * this.Reaper.batSpeed);
        this.Reaper.SetTargetVelocityVertical(dashDirection.y * this.Reaper.batSpeed);
        // this.Reaper.MoveVertical(this.Reaper.jumpSpeed * Time.fixedDeltaTime);
    }

    public override void Exit() { 
        // InputController.Instance.UseJump();
    }

    public override void CheckState()
    {
        if(timeActive > this.Reaper.batTime)
        {
            this.Reaper.ChangeState(this.Reaper.FallState);
            return;
        }
    //     if(!this.Reaper.Inputs.Jump && jumpTime > jumpTimeMin)
    //     {
    //         this.Reaper.ChangeState(this.Reaper.FallState);
    //         return;
    //     }

    //     if (this.Reaper.OnGround && jumpTime > jumpTimeMin)
    //     {
    //         Debug.Log("Idle");
    //         // THIS IS GONIG TO BE THE LAND STATE
    //         this.Reaper.ChangeState(this.Reaper.RunState);
    //         return;
    //     }

    //     if(jumpTime >= this.Reaper.jumpTime)
    //     {
    //         Debug.Log("jumpTime");
    //         this.Reaper.ChangeState(this.Reaper.FallState);
    //         return;
    //     }

    }
}
