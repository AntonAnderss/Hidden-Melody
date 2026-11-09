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
        if (!stateMachine.PlayerMovement.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.idleState);
            return;
        }

        // Starts to move while crouched
        if (stateMachine.PlayerMovement.IsWalking)
        {
            stateMachine.ChangeState(stateMachine.crouchState);
        }
    }
}