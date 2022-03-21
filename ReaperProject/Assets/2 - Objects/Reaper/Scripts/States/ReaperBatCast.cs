// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ReaperBatCast : ReaperState
// {
//     public ReaperBatCast(ReaperController _reaper) : base(_reaper) { 
//         this.TargetDir = Vector2.right;
//     }

//     private float timeSinceActive = 0;
//     public Vector2 TargetDir { get; set; }

//     public override void Enter()
//     {
//     }

//     public override void FrameUpdate()
//     {
//         // timeSinceActive += Time.deltaTime;

//         // if (Reaper.Inputs.BatDash)
//         // {
//         //     if (timeSinceActive >= Reaper.dashMaxCastTime)
//         //     {
//         //         Reaper.BatDashState.TargetDir = TargetDir;
//         //         Reaper.BatDashState.Distance = Reaper.dashDistance;
//         //         Reaper.ChangeState(Reaper.BatDashState);
//         //         return;
//         //     }
//         // }
//         // else
//         // {
//         //     Reaper.BatDashState.TargetDir = TargetDir;
//         //     float dist = Reaper.Inputs.BatDashEndTime - Reaper.Inputs.BatDashStartTime;
//         //     Reaper.BatDashState.Distance = Reaper.dashDistance * dist;
//         //     Reaper.ChangeState(Reaper.BatDashState);
//         // }

//         // TargetDir = Reaper.Inputs.Dir;
//     }

//     public override void PhysicsUpdate() { }

//     public override void Exit() { }
// }
