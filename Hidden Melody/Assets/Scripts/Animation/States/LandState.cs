using UnityEngine;

public class LandState : BaseState
{
    public LandState(StateMachine stateMachine)
        : base("LandState", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER LAND");
        stateMachine.Animator.SetInteger("state", 5);
    }

    public override void Update()
    {
        base.Update();

        AnimatorStateInfo info =
            stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 0.9f &&
            !stateMachine.Animator.IsInTransition(0))
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
