using UnityEngine;
using System;

[Serializable]
public abstract class Ability
{
    public abstract AbilityType Type { get; }

    public void Unlock()
    {
        AbilityUnlockState.Unlock(Type);
    }

    public void Use()
    {
        if (!AbilityUnlockState.IsUnlocked(Type))
        {
            Debug.Log(Type + " is locked");
            return;
        }

        Activate();
    }
    protected abstract void Activate();
}
