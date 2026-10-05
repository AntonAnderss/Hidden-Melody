using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Ability", menuName = "Music Objects/Ability", order = 1)]
public class Ability : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public bool Unlocked;

    [Header("Logic")]
    public Note[] Combination;
    public virtual void Perform() { Debug.LogError("Hasn't overriden Ability"); }
}

[CreateAssetMenu(fileName = "Debug Ability", menuName = "Music Objects/Debug Ability", order = 1)]
public class DebugAbility : Ability
{
    public override void Perform() { Debug.Log("Performed Debug"); }
}
