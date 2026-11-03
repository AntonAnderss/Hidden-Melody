using System;
using System.Collections;
using System.Collections.Generic;
/* Author: Anton Andersson */
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Music Objects/Ability", order = 1)]
public class Ability : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public bool Unlocked;
    public string Description;

    [Header("Logic")]
    public Note[] Combination;
    public virtual void Perform() { Debug.LogError("Hasn't overriden Ability"); }
    public void Unlock() { Unlocked = true; }
}