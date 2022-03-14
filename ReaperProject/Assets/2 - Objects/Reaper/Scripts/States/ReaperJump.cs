using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperJump : ReaperState
{
    public ReaperJump(ReaperController _reaper) : base(_reaper) { }

    private float timeSinceActive = 0;
    private Vector2 startPosition = Vector2.zero;

    public float JumpPercentage { get { return 1 - ((Reaper.jumpTime - (timeSinceActive)) / Reaper.jumpTime); } }
    public float JumpValue { get { return Reaper.jumpCurve.Evaluate(JumpPercentage); } }

    public override void Enter()
    {
        InputController.Instance.EndJump();
        Reaper.UseJump();
        startPosition = Reaper.rb.position;
        timeSinceActive = 0;
    }
    public override void FrameUpdate()
    {
        timeSinceActive += Time.deltaTime;

        if (Reaper.OnGround && timeSinceActive >= 0.3f)
        {
            Reaper.ChangeState(Reaper.IdleState);
            return;
        }

        if (JumpPercentage >= 1f || Reaper.OnTop)
        {
            Reaper.ChangeState(Reaper.FallingState);
            return;
        }

        if (Reaper.Inputs.Jump && Reaper.CanJump)
        {
            Reaper.ChangeState(Reaper.JumpingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        Vector2 newPosition = startPosition;
        newPosition.x = Reaper.rb.position.x + Reaper.Inputs.Dir.x * Reaper.jumpMovementSpeed * Time.fixedDeltaTime;
        newPosition.y += JumpValue * Reaper.jumpHeight * Reaper.Inputs.JumpStrength * Time.fixedDeltaTime * 100;

        Reaper.rb.MovePosition(newPosition);

        if (JumpValue >= 0.9f)
            Reaper.ChangeState(Reaper.FallingState);
    }

    public override void Exit() { }
}
