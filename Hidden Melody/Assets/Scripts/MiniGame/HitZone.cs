using UnityEngine;

public class HitZone
{
   

    public float minValue;
    public float maxValue;
    public bool completed;

    public Note requiredNote;
    // blueprint for hitzones
    public HitZone(float min, float max, Note note)
    {
        minValue = min;
        maxValue = max;
        completed = false;
        requiredNote = note;
    }
}
