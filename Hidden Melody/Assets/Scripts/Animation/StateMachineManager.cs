using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    protected BaseState currentState;
    public bool LockedInIdle { get; set; }

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter();
        }
    }


    void Update()
    {
        if (LockedInIdle)
            return;

        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (newState == null || newState == currentState) return;
        Debug.Log("What state am i in? : " + newState);
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
