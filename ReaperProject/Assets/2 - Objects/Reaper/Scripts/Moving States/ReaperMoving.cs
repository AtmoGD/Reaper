using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperMoving : ReaperState
{
    // protected float lastMovingSinceStart = 0f;
    // protected float horizontalMultiplier = 0f;
    protected Vector2 targetDirection = Vector2.zero;
    private float coyotyTimer = 0f;

    public ReaperMoving(ReaperController _reaper) : base(_reaper)
    {
        this.targetDirection = Reaper.rb.position;
    }

    public override void Enter(float useTimeSinceStart = 0f)
    {
        base.Enter(useTimeSinceStart);
        this.targetDirection = Vector2.zero;
    }

    public override void FrameUpdate()
    {
        if (this.Reaper.Inputs.Jump && this.Reaper.CanJump)
        {
            this.Reaper.ChangeState(this.Reaper.JumpingState);
            return;
        }


        if (this.Reaper.OnGround)
        {
            coyotyTimer = this.Reaper.coyotyTime;
        }
        else
        {
            coyotyTimer -= Time.deltaTime;
            
            if(coyotyTimer <= 0f)
            {
                this.Reaper.ChangeState(this.Reaper.FallingState);
                return;
            }
        }

        if (this.Reaper.Inputs.Dir.magnitude <= this.Reaper.movementThreshold)
        {
            this.Reaper.ChangeState(this.Reaper.IdleState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        this.TimeSinceStart += Time.fixedDeltaTime;

        float multiplier = this.Reaper.accelerationCurve.Evaluate(this.TimeSinceStart);

        this.MoveHorizontal(multiplier);

        this.Move();
    }

    protected void MoveInDirection(Vector2 _dir)
    {
        this.targetDirection += _dir;
    }

    protected void MoveHorizontal(float _multiplier = 1)
    {
        // this.Reaper.LastMovementStartTime = _multiplier;
        float amount = this.Reaper.Inputs.Dir.x * this.Reaper.accelerationMultiplier * _multiplier;
        // Debug.Log("Hori: " + amount);
        this.targetDirection += Vector2.right * amount * Time.deltaTime;
    }

    protected void MoveVertical(float _amount)
    {
        float amount = _amount;
        // Debug.Log("Vert: " + amount);
        this.targetDirection += Vector2.up * amount * Time.deltaTime;
    }

    private void Move()
    {
        this.Reaper.rb.velocity = Vector2.Lerp(this.Reaper.rb.velocity, this.targetDirection, this.Reaper.lerpMultiplier * Time.fixedDeltaTime);
        this.targetDirection = Vector2.zero;
    }

    public override void Exit() { }
}
