using UnityEngine;

public class HitZone
{
   

    public float minValue;
    public float maxValue;
    public bool completed;

    public KeyCode requiredKey;
    // blueprint for hitzones
    public HitZone(float min, float max, KeyCode key)
    {
        minValue = min;
        maxValue = max;
        completed = false;
        requiredKey = key;
    }
}
