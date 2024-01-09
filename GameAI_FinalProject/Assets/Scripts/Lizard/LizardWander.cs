using UnityEngine;
using UnityEngine.AI;

public class LizardWander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float minWanderInterval = 5f; // Minimum time between wanderings
    public float maxWanderInterval = 10f; // Maximum time between wanderings

    private NavMeshAgent agent;
    private float timer;
    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewWanderTimer();
        Wander();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Wander();
            SetNewWanderTimer();
        }
    }

    void SetNewWanderTimer()
    {
        wanderTimer = Random.Range(minWanderInterval, maxWanderInterval);
        timer = wanderTimer;
    }

    void Wander()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        if (newPos != Vector3.positiveInfinity)
        {
            agent.SetDestination(newPos);
        }
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }

        return Vector3.positiveInfinity;
    }
}
