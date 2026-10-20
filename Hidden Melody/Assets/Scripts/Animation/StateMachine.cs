using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateMachine : StateMachineManager
{
    public Animator Animator { get; private set; }
    public IdleState idleState;
    public WalkState walkState;
    public BaseState pendingNextState;
    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        idleState = new IdleState(this);
        walkState = new WalkState(this);
        //patrollingState = new PatrollingState(this);
        //layingDownState = new LayingDownState(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    public void OnGetUpFinished()
    {
        if (pendingNextState != null)
        {
            Debug.Log("GetUp finished");
            ChangeState(pendingNextState);
            pendingNextState = null;
        }
    }

}
