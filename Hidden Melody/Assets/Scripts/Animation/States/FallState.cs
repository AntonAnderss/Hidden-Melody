using UnityEngine;
public class FallState : BaseState
{
    public FallState(StateMachine stateMachine)
        : base("FallState", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Animator.SetInteger("state", 4);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.PlayerMovement.grounded)
        {
            stateMachine.ChangeState(stateMachine.landState);
        }
    }
}
