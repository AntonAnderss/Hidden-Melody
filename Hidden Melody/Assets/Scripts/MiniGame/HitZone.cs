using UnityEngine;

public class HitZone
{
   

    public float minValue;
    public float maxValue;
    public bool completed;
    // blueprint for hitzones
    public HitZone(float min, float max)
    {
        minValue = min;
        maxValue = max;
        completed = false;
    }
}
