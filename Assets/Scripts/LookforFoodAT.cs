using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class SearchForFoodAT : ActionTask<NavMeshAgent>
{
    public BBParameter<GameObject> foodTarget;
    public float detectRadius = 10f;
    public float wanderRadius = 5f;

    private Vector3 targetPosition;

    protected override void OnExecute()
    {

        PickNewWanderDestination();
    }

    protected override void OnUpdate()
    {

        Collider[] hits = Physics.OverlapSphere(agent.transform.position, detectRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Seed"))
            {
                foodTarget.value = hit.gameObject;
                agent.SetDestination(hit.transform.position);
                break;
            }
        }

        if (foodTarget.value != null)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                EndAction(true);
                return;
            }
        }
        else
        {

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                PickNewWanderDestination();
        }
    }

    private void PickNewWanderDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += agent.transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
            agent.SetDestination(targetPosition);
        }
    }
}