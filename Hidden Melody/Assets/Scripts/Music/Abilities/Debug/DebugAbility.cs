/* Author: Anton Andersson */
using UnityEngine;

[CreateAssetMenu(fileName = "Debug Ability", menuName = "Music Objects/Debug Ability", order = 1)]
public class DebugAbility : Ability
{
    public override void Perform() { Debug.Log("Performed Debug"); }
}
