using UnityEngine;

public class IdleState : BaseState
{

    private StateMachine stateMachine;

    public IdleState(StateMachine stateMachine) : base("IdleState", stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Animator.SetInteger("state", 1);
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            stateMachine.ChangeState(stateMachine.walkState);
        }
    }

    public override void Exit()
    {

        base.Exit();

    }

}
