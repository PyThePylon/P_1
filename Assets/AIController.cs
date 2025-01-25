using UnityEngine;
using UnityEngine.AI;

public abstract class AIState
{
    public abstract void EnterState(AIController ai); 
    public abstract void UpdateState(AIController ai); 
    public abstract void ExitState(AIController ai); 
}

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float stalkingDistance = 5f;
    public Vector3 lastKnownPlayerPosition;

    public AIState currentState;

    public IdleState idleState = new IdleState();
    public StalkingState stalkingState = new StalkingState();
    public SearchingState searchingState = new SearchingState();
    public TauntingState tauntingState = new TauntingState();

    public float stalkingMinDistance = 3f; 
    public float stalkingMaxDistance = 7f; 

    public bool hasSeenPlayer = false; 
    public float stalkingSpeed = 2f; 
    public float backOffDistance = 2f; 
    public float circlingAngle = 30f;

    public Dialouge dialogueManager;


    private void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);

        dialogueManager = GetComponent<Dialouge>();

    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AIState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        if (newState == idleState)
        {
            // Example: Gradual stop
            agent.isStopped = false;
            agent.speed = Mathf.Lerp(agent.speed, 0f, Time.deltaTime * 2f);
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    public bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer < 45f && Vector3.Distance(transform.position, player.position) < 10f)
        {
            if (!Physics.Linecast(transform.position, player.position, out RaycastHit hit) || hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsCloseToPlayer()
    {
        return Vector3.Distance(transform.position, player.position) < 3f;
    }

    public void TauntPlayer()
    {
        Debug.Log("The AI is taunting the player!");
    }
}
