using UnityEngine;

public class CrouchState : BaseState
{
    public CrouchState(StateMachine stateMachine)
        : base("CrouchState", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.Animator.SetInteger("state", 6);
    }

    public override void Update()
    {
        base.Update();

        // Stops moving while crouching
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            stateMachine.ChangeState(stateMachine.crouchIdleState);
        }
        if (!stateMachine.PlayerMovement.IsCrouching)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                stateMachine.ChangeState(stateMachine.walkState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.idleState);
            }

        }
    }
}