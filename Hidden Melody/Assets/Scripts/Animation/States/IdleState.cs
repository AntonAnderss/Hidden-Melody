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

        // Player starts a jump
        if (!stateMachine.PlayerMovement.IsGrounded)
        {
            if (stateMachine.Rigidbody.linearVelocity.y > 0.1f)
            {
                stateMachine.ChangeState(stateMachine.jumpState);
            }
            else if (stateMachine.Rigidbody.linearVelocity.y < -0.1f)
            {
                stateMachine.ChangeState(stateMachine.fallState);
            }

            return;
        }
        // Crouch 
        if (stateMachine.PlayerMovement.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.crouchIdleState);
            return;
        }
        // Player falls from an edge
        if (!stateMachine.PlayerMovement.IsGrounded &&
            stateMachine.Rigidbody.linearVelocity.y <= 0f)
        {
            stateMachine.ChangeState(stateMachine.fallState);
            return;
        }

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
