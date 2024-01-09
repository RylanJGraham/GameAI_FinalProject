using UnityEngine;
using UnityEngine.AI;

public class JaguarPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetDestination();
    }
    
    void SetDestination()
    {
        if (waypoints.Length > 0)
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];
            agent.SetDestination(currentWaypoint.position);
        }
    }
    
    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = Random.Range(0, waypoints.Length);
            SetDestination();
        }
    }
}
