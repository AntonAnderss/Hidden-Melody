using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(StateMachine stateMachine)
        : base("JumpState", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Animator.SetInteger("state", 3);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Rigidbody.linearVelocity.y < -0.1f)
        {
            stateMachine.ChangeState(stateMachine.fallState);
        }
    }
}
