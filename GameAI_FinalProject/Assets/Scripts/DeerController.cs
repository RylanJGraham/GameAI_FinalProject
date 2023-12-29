using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DeerController : MonoBehaviour
{
    public float maxDistance = 10f; // Set the maximum distance here
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
            MoveRandom();
        }
    }

    void MoveRandom()
    {
        DeerManager deerManager = FindObjectOfType<DeerManager>();
        if (deerManager != null)
        {
            Vector3 randomPoint = Random.insideUnitCircle * deerManager.circleRadius;
            randomPoint.z = randomPoint.y;
            randomPoint.y = 0; // Ensure the point is on the same plane as the ground

            Vector3 destination = deerManager.transform.position + randomPoint;

            NavMeshHit hit;
            NavMesh.SamplePosition(destination, out hit, maxDistance, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
        }
    }
}
