using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateMachine : StateMachineManager
{
    public Animator Animator { get; private set; }
    [System.NonSerialized] public IdleState idleState;
    [System.NonSerialized] public WalkState walkState;
    [System.NonSerialized] public BaseState pendingNextState;
    public PlayerMovement PlayerMovement { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    [System.NonSerialized] public JumpState jumpState;
    [System.NonSerialized] public LandState landState;
    [System.NonSerialized] public FallState fallState;
    [System.NonSerialized] public CrouchState crouchState;
    [System.NonSerialized] public CrouchIdleState crouchIdleState;
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
