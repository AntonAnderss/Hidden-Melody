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
        if (!stateMachine.PlayerMovement.grounded)
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
        if (!stateMachine.PlayerMovement.grounded)
        {
            if (stateMachine.Rigidbody.linearVelocity.y > 0.1f)
                stateMachine.ChangeState(stateMachine.jumpState);
            else
                stateMachine.ChangeState(stateMachine.fallState);

            return;
        }

        // Crouch state
        if (stateMachine.PlayerMovement.crouching)
        {
            stateMachine.ChangeState(stateMachine.crouchState);
            return;
        }
        // If player stops moving
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
