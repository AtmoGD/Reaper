using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReaperState
{
    protected ReaperController Reaper { get; set; }
    public ReaperState(ReaperController _reaper) => this.Reaper = _reaper;
    public abstract void Enter();
    public virtual void FrameUpdate() {
        CheckState();
    }
    
    public abstract void PhysicsUpdate();
    public abstract void Exit();
    public abstract void CheckState();
}
