using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperMoving : ReaperState
{
    private float coyotyTimer = 0f;

    public ReaperMoving(ReaperController _reaper) : base(_reaper) { }

    public override void Enter() { }

    public override void FrameUpdate()
    {
        if (this.Reaper.Inputs.Jump && this.Reaper.CanJump)
        {
            this.Reaper.ChangeState(this.Reaper.JumpingState);
            return;
        }


        // if (this.Reaper.OnGround)
        // {
        //     coyotyTimer = this.Reaper.coyotyTime;
        // }
        // else
        // {
        //     coyotyTimer -= Time.deltaTime;
            
        //     if(coyotyTimer <= 0f)
        //     {
        //         this.Reaper.ChangeState(this.Reaper.FallingState);
        //         return;
        //     }
        // }

        // if (this.Reaper.Inputs.Dir.magnitude <= this.Reaper.movementThreshold)
        // {
        //     this.Reaper.ChangeState(this.Reaper.IdleState);
        //     return;
        // }
    }

    public override void PhysicsUpdate()
    {
        this.Reaper.MoveHorizontal(this.Reaper.speed * this.Reaper.Inputs.Dir.x);
        // this.Reaper.Move(Vector2.right * this.Reaper.Inputs.Dir.x, 
        //                 this.Reaper.speed, 
        //                 this.Reaper.movementLerpSpeed,
        //                 this.Reaper.maxSpeed,
        //                 Mathf.Infinity);
    }

    public override void Exit() { }
}
