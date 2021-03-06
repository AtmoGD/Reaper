using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperMovement : ReaperState
{
    public ReaperMovement(ReaperController _reaper) : base(_reaper) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (Mathf.Abs(this.Reaper.Inputs.Dir.x) >= 0.3) {
            this.Reaper.RotateModel(Mathf.Abs(this.Reaper.rb.velocity.x * this.Reaper.rotatingSpeed));
            
        }
    }

    public override void PhysicsUpdate()
    {
        if (this.Reaper.Inputs == null) return;

        float speed = this.Reaper.speed * this.Reaper.Inputs.Dir.x * Time.fixedDeltaTime;

        this.Reaper.animator.SetFloat("SpeedX", Mathf.Abs(speed));

        if (speed == 0)
        {
            this.Reaper.SetTargetVelocityHorizontal(0f, this.Reaper.stopSpeed * Time.fixedDeltaTime);
        }
        else
        {
            this.Reaper.MoveHorizontal(speed);
        }
    }

    public override void Exit()
    {
        // base.Exit();
    }

    public override void CheckState()
    {
        if (this.Reaper.Inputs.Bats)
        {
            this.Reaper.ChangeState(this.Reaper.BatState);
            return;
        }

        if (this.Reaper.JumpsLeft < this.Reaper.jumpCount && this.Reaper.OnGround)
        {
            this.Reaper.ResetJump();
        }

        if (this.Reaper.Inputs.Jump && this.Reaper.CanJump)
        {
            this.Reaper.ChangeState(this.Reaper.JumpState);
            return;
        }

    }
}
