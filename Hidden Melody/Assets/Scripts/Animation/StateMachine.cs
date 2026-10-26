using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateMachine : StateMachineManager
{
    public Animator Animator { get; private set; }
    public IdleState idleState;
    public WalkState walkState;
    public BaseState pendingNextState;
    public PlayerMovement PlayerMovement { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public JumpState jumpState;
    public LandState landState;
    public FallState fallState;
    public CrouchState crouchState;
    public CrouchIdleState crouchIdleState;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PlayerMovement = GetComponent<PlayerMovement>();
        Animator = GetComponentInChildren<Animator>();
        idleState = new IdleState(this);
        walkState = new WalkState(this);
        fallState = new FallState(this);
        jumpState = new JumpState(this);
        landState = new LandState(this);
        crouchIdleState = new CrouchIdleState(this);
        crouchState = new CrouchState(this);

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
            ChangeState(pendingNextState);
            pendingNextState = null;
        }
    }
}
