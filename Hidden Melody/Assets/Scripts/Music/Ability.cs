using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Ability", menuName = "Music Objects/Ability", order = 1)]
public class Ability :ScriptableObject
{
    [Header("Info")]
    public string Name;
    public bool Unlocked;

    [Header("Logic")]
    public Note[] combination;
    public MonoBehaviour script;
    public UnityEvent Logic;
}
