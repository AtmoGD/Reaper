using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperJump : ReaperMoving
{
    public ReaperJump(ReaperController _reaper) : base(_reaper) { }

    private float timeSinceActive = 0f;

    public override void Enter()
    {
        base.Enter();

        this.Reaper.Jump();

        this.Reaper.UseJump();

        Debug.Log("Reaper Jump");
        
        timeSinceActive = 0f;
    }

    public override void FrameUpdate() {

        if (this.Reaper.Inputs.Jump && this.Reaper.CanJump)
        {
            this.Reaper.ChangeState(this.Reaper.JumpingState);
            return;
        }

        timeSinceActive += Time.deltaTime;

        if (this.Reaper.OnGround && this.Reaper.JumpCooldown <= 0f)
        {
            Debug.Log("Reaper is on ground and is not moving up");
            this.Reaper.ChangeState(this.Reaper.MovingState);
            return;
        }

        // if(timeSinceActive >= this.Reaper.jumpTime) {
        //     Debug.Log("Reaper has jumped for " + timeSinceActive + " seconds");
        //     this.Reaper.ChangeState(this.Reaper.MovingState);
        //     return;
        // }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // this.Reaper.Move(Vector2.up, 
        //                 this.Reaper.jumpSpeed * this.Reaper.jumpCurve.Evaluate(timeSinceActive), 
        //                 this.Reaper.jumpLerpSpeed,
        //                 this.Reaper.maxSpeed,
        //                 this.Reaper.maxJumpSpeed);
    }

    public override void Exit()
    {
        

        base.Exit();
    }
}
