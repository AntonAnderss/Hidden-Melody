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


public class TestInstrument : Instrument
{
    public override void InitInstrument()
    {


        combinationToString.Add("Dash", new[] { Note.One, Note.Two, Note.Three });
        combinationToString.Add("DoubleJump", new[] { Note.Three, Note.Two, Note.One });

        stringToAbility.Add("Dash", new DashAbility());
        stringToAbility.Add("DoubleJump", new DoubleJumpAbility());
    }
}
