using UnityEngine;

public class CrouchIdleState : BaseState
{
    public CrouchIdleState(StateMachine stateMachine)
        : base("CrouchIdleState", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.Animator.SetInteger("state", 7);
    }

    public override void Update()
    {
        base.Update();

        // Stops crouching
        if (!stateMachine.PlayerMovement.crouching)
        {
            stateMachine.ChangeState(stateMachine.idleState);
            return;
        }

        // Starts to move while crouched
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            stateMachine.ChangeState(stateMachine.crouchState);
        }
    }
}