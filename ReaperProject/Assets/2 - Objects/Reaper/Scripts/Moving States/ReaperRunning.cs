
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class ReaperRunning : ReaperMoving
// {
//     public ReaperRunning(ReaperController _reaper) : base(_reaper) { }

//     public override void Enter()
//     {
//         base.Enter();
//     }

//     public override void FrameUpdate()
//     {
//         // base.FrameUpdate();
//         if (this.Reaper.Inputs.Jump && this.Reaper.CanJump)
//         {
//             this.Reaper.ChangeState(this.Reaper.JumpingState);
//             return;
//         }

//         if (!this.Reaper.OnGround)
//         {
//             this.Reaper.ChangeState(this.Reaper.MovingState);
//             return;
//         }

//         if (this.Reaper.Inputs.Dir.magnitude <= this.Reaper.movementThreshold)
//         {
//             this.Reaper.ChangeState(this.Reaper.MovingState);
//             return;
//         }
//     }

//     public override void PhysicsUpdate()
//     {
//         // float multiplier = this.Reaper.accelerationCurve.Evaluate(this.timeSinceStart);

//         // this.MoveHorizontal(multiplier);

//         // this.MoveVertical(0f);

//         base.PhysicsUpdate();
//     }

//     public override void Exit()
//     {
//         base.Exit();
//     }
// }
