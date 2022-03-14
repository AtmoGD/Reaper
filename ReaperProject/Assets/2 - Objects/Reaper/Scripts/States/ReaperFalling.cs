using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperFalling : ReaperState
{
    public ReaperFalling(ReaperController _reaper) : base(_reaper) { }

    public override void Enter() { }

    public override void FrameUpdate()
    {
        if (Reaper.OnGround)
        {
            Reaper.ChangeState(Reaper.IdleState);
        }

        if (Reaper.Inputs.Jump && Reaper.CanJump)
        {
            Debug.Log("DoubleJump");
            Debug.Log(Reaper.JumpsLeft);
            Reaper.ChangeState(Reaper.JumpingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        Vector2 newPosition = Reaper.rb.position;
        newPosition.x += Reaper.Inputs.Dir.x * Reaper.speed * Time.fixedDeltaTime;
        newPosition.y -= Reaper.fallSpeed * Time.fixedDeltaTime;

        Reaper.rb.MovePosition(newPosition);
    }

    public override void Exit() { }
}
