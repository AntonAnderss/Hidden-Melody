using UnityEngine;

public class TestAbility : OldAbility
{
    public override AbilityType Type => AbilityType.Debug;
    //public override void Use() { Debug.Log("Played Test Ability"); }
    protected override void Activate()
    {
        Debug.Log("Debug 1");
    }
}

public class TestTwoAbility : OldAbility
{
    public override AbilityType Type => AbilityType.Debug;
    protected override void Activate()
    {
        Debug.Log("Debug 2");
    }

}

public class NoteTestAbility : OldAbility
{
    public override AbilityType Type => AbilityType.Debug;
    protected override void Activate() { Debug.Log("Debug Note"); }
}


public class DebugInstrument : OldInstrument
{
    public override void InitInstrument()
    {
        combinationToAbility.Add(new[] { Note.One, Note.Two, Note.Three }, new TestAbility());
        combinationToAbility.Add(new[] { Note.Three, Note.Two, Note.One }, new TestTwoAbility());

        noteToAbility.Add(Note.One, new NoteTestAbility());

        foreach(var a in combinationToAbility.Keys)
        {
            combinationToAbility[a].Unlock();
        }
    }
}

