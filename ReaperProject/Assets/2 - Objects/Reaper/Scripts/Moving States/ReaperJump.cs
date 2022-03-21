using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperJump : ReaperMoving
{
    public ReaperJump(ReaperController _reaper) : base(_reaper) { }

    
    // private float verticalMultiplier = 0f;


    public override void Enter(float useTimeSinceStart = 0f)
    {
        base.Enter();

        
        // verticalMultiplier = 0f; 
        // this.Reaper.rb.AddForce(Vector2.up * this.Reaper.jumpForce, ForceMode2D.Impulse);
        this.targetDirection += Vector2.up * this.Reaper.jumpForce * this.Reaper.Inputs.JumpStrength;
        // this.Reaper.ChangeState(this.Reaper.MovingState);
    }

    public override void FrameUpdate()
    {
        if (this.TimeSinceStart >= Reaper.jumpTime)
        {
            this.Reaper.ChangeState(this.Reaper.MovingState);
            return;
        }

        // if (this.Reaper.OnGround && this.TimeSinceStart > 0.1f)
        // {
        //     this.Reaper.ChangeState(this.Reaper.MovingState);
        //     return;
        // }
    }

    public override void PhysicsUpdate()
    {
        // this.MoveHorizontal();
        // this.horizontalMultiplier = this.Reaper.accelerationCurve.Evaluate(this.timeSinceStart);
        // float verticalMultiplier = this.Reaper.jumpCurve.Evaluate(this.TimeSinceStart);
        // Vector2 dir = Vector2.right * horizontalMultiplier + Vector2.up * verticalMultiplier;
        // Debug.Log("Jump: " + dir);
        // Debug.Log("Jump: " + verticalMultiplier);
        // this.MoveHorizontal(horizontalMultiplier);
        // this.MoveVertical(verticalMultiplier * this.Reaper.jumpForce);

        // this.targetDirection.x = Mathf.Clamp(this.targetDirection.x, -Reaper.maxSpeed, Reaper.maxSpeed);

        // this.MoveVertical(this.Reaper.jumpForce * multiplier);

        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        this.Reaper.UseJump();
        InputController.Instance.EndJump();
        base.Exit();
    }
}
