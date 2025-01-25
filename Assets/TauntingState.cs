using UnityEngine;

public class TauntingState : AIState
{
    public override void EnterState(AIController ai)
    {
        Debug.Log("AI entered Taunting state.");
        ai.dialogueManager.PlayTauntDialogue();

        ai.agent.isStopped = true; 
        ai.TauntPlayer(); 
    }

    public override void UpdateState(AIController ai)
    {
        if (!ai.IsCloseToPlayer())
        {
            ai.SwitchState(ai.stalkingState);
        }
    }

    public override void ExitState(AIController ai)
    {
        Debug.Log("AI exiting Taunting state.");
    }
}
