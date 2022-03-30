using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReaperState
{
    protected float timeSinceActive = 0f;
    protected ReaperController Reaper { get; set; }
    public ReaperState(ReaperController _reaper) => this.Reaper = _reaper;
    public virtual void Enter() {
        this.timeSinceActive = 0f;
    }
    public virtual void FrameUpdate() {
        this.timeSinceActive += Time.deltaTime;
        CheckState();
    }
    
    public abstract void PhysicsUpdate();
    public abstract void Exit();
    public abstract void CheckState();
}
