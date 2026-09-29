using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityLogic : MonoBehaviour
{
    public virtual void Perform() { Debug.LogError("Logic not overwritten"); }
}
