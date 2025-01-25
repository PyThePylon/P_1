using UnityEngine;

public class IdleState : AIState
{
    public override void EnterState(AIController ai)
    {
        Debug.Log("AI entered Idle state.");
        ai.agent.isStopped = true; 
    }

    public override void UpdateState(AIController ai)
    {
        if (ai.CanSeePlayer())
        {
            ai.SwitchState(ai.stalkingState);
        }
    }

    public override void ExitState(AIController ai)
    {
        Debug.Log("AI exiting Idle state.");
    }
}
