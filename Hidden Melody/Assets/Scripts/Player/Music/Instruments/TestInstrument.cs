using UnityEngine;

//public class TestAbility : Ability
//{
//    //public override void Use() { Debug.Log("Played Test Ability"); }
//    protected override void Activate()
//    {
//        Debug.Log("TestAbility1");
//    }
//}

//public class TestTwoAbility : Ability
//{
//    //public override void Use() { Debug.Log("Played TestTwo Ability"); }
//    protected override void Activate()
//    {
//        Debug.Log("Testability2");
//    }

//}

//public class NoteTestAbility : Ability
//{
    //public override void Use() { Debug.Log("Played Note Test Ability"); }
//}


public class TestInstrument : Instrument
{
    public override void InitInstrument()
    {
        //combinationToAbility.Add(new[] { Note.One, Note.Two, Note.Three }, new TestAbility());
        //combinationToAbility.Add(new[] { Note.Three, Note.Two, Note.One }, new TestTwoAbility());

        //noteToAbility.Add(Note.One, new NoteTestAbility());

        //combinationToString.Add("Test", new[] { Note.One, Note.Two, Note.Three });
        //combinationToString.Add("Test2", new[] { Note.Three, Note.Two, Note.One });

        //stringToAbility.Add("Test", new TestAbility());
        //stringToAbility.Add("Test2", new TestTwoAbility());


        combinationToString.Add("Dash", new[] { Note.One, Note.Two, Note.Three });
        combinationToString.Add("DoubleJump", new[] { Note.Three, Note.Two, Note.One });

        stringToAbility.Add("Dash", new DashAbility());
        stringToAbility.Add("DoubleJump", new DoubleJumpAbility());
    }
}
