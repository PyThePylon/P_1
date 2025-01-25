using UnityEngine;

public class SearchingState : AIState
{
    public override void EnterState(AIController ai)
    {
        Debug.Log("AI entered Searching state.");
        ai.agent.isStopped = false;
        ai.agent.SetDestination(ai.lastKnownPlayerPosition);

        ai.dialogueManager.PlaySearchDialogue();
    }

    public override void UpdateState(AIController ai)
    {
        if (ai.CanSeePlayer())
        {
            ai.SwitchState(ai.stalkingState);
        }
        else if (ai.agent.remainingDistance <= ai.agent.stoppingDistance)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Vector3 searchPosition = ai.lastKnownPlayerPosition + randomOffset;

            ai.agent.SetDestination(searchPosition);
            ai.lastKnownPlayerPosition = searchPosition;

            ai.SwitchState(ai.idleState);
        }
    }

    public override void ExitState(AIController ai)
    {
        Debug.Log("AI exiting Searching state.");
    }
}
