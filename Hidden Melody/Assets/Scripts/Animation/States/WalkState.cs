using UnityEngine;

public class WalkState : BaseState
{
    public WalkState(StateMachine stateMachine) : base("WalkState", stateMachine){}

    public override void Enter()
    {
        base.Enter();

        stateMachine.Animator.SetInteger("state", 2);
    }

    public override void Update()
    {
        base.Update();

        // Om spelaren slutar röra sig
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
