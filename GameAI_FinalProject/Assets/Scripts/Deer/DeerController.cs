using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DeerController : MonoBehaviour
{
    NavMeshAgent agent;
    DeerManager deerManager;

    public void Initialize(DeerManager manager)
    {
        deerManager = manager;
        agent = GetComponent<NavMeshAgent>();
        MoveRandom();
    }

    void MoveRandom()
    {
        if (deerManager != null)
        {
            Vector3 randomDestination = deerManager.transform.position + Random.insideUnitSphere * 20f; // Adjust the radius to match the zone
            randomDestination.y = 0f; // Ensure movement on the ground level

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDestination, out hit, 20f, NavMesh.AllAreas); // Adjust sample distance as needed
            agent.SetDestination(hit.position);
        }
    }

    void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
            MoveRandom();
        }
    }
}
