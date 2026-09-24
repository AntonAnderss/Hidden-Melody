using UnityEngine;

public class Timer
{
    public float time = 0.0f;
    public float targetTime = 0.0f; 

    public Timer(float targetTime) { this.targetTime = targetTime; }

    public bool TargetTimeReached()
    {
        time += Time.deltaTime;

        if (targetTime > time)
            return false;

        Reset();
        return true;
    }

    public void Reset() { time = 0.0f; }
}
