using UnityEngine;

public class StalkingState : AIState
{
    public override void EnterState(AIController ai)
    {
        Debug.Log("AI entered Stalking state.");
        ai.agent.isStopped = false; 
        ai.hasSeenPlayer = true;

        ai.dialogueManager.PlayStalkDialogue();
    }

    public override void UpdateState(AIController ai)
    {
        Vector3 playerPosition = ai.player.position;
        float distanceToPlayer = Vector3.Distance(ai.transform.position, playerPosition);

        if (distanceToPlayer < ai.stalkingMinDistance)
        {
            Vector3 directionAway = (ai.transform.position - playerPosition).normalized;
            Vector3 targetPosition = ai.transform.position + directionAway * ai.backOffDistance;
            ai.agent.SetDestination(targetPosition);
            ai.agent.speed = ai.stalkingSpeed; 
        }
        else if (distanceToPlayer > ai.stalkingMinDistance && distanceToPlayer <= ai.stalkingMaxDistance)
        {
            Vector3 offset = Quaternion.Euler(0, ai.circlingAngle, 0) * (playerPosition - ai.transform.position);
            Vector3 stalkingPosition = playerPosition + offset.normalized * ai.stalkingMinDistance;
            ai.agent.SetDestination(stalkingPosition);
            ai.agent.speed = ai.stalkingSpeed;
        }
        else if (distanceToPlayer > ai.stalkingMaxDistance)
        {
            ai.agent.SetDestination(playerPosition);
            ai.agent.speed = ai.stalkingSpeed;
        }

        if (!ai.CanSeePlayer())
        {
            ai.SwitchState(ai.searchingState);
        }

        if (ai.IsCloseToPlayer())
        {
            ai.SwitchState(ai.tauntingState);
        }
    }

    public override void ExitState(AIController ai)
    {
        Debug.Log("AI exiting Stalking state.");
    }
}
