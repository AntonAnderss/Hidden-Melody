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
        // player leaves the ground
        if (!stateMachine.PlayerMovement.IsGrounded)
        {
            if (stateMachine.Rigidbody.linearVelocity.y > 0.1f)
            {
                stateMachine.ChangeState(stateMachine.jumpState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.fallState);
            }

            return;
        }

        // Crouch state
        if (stateMachine.PlayerMovement.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.crouchState);
            return;
        }
        // If player stops moving
        if (!stateMachine.PlayerMovement.IsWalking)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
