using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReaperState
{
    protected ReaperController Reaper { get; set; }
    protected float TimeSinceStart { get; set; }
    public ReaperState(ReaperController _reaper)
    {
        this.Reaper = _reaper;
        this.TimeSinceStart = 0f;
    }
    public virtual void Enter(float useTimeSinceStart = 0f) {
        this.TimeSinceStart = useTimeSinceStart;
    }
    public abstract void FrameUpdate();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
}
