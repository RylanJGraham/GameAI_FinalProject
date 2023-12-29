using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerManager : MonoBehaviour
{
    public GameObject deerPrefab; // Reference to the deer prefab
    public int numberOfDeer = 5; // Number of deer to spawn
    public float circleRadius = 10f; // Set the initial circle radius
    public Transform spawnPoint; // Settable spawn point for deer
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveCircle();
        SpawnDeer();
    }

    void MoveCircle()
    {
        Vector3 randomPoint = Random.insideUnitSphere * circleRadius;
        randomPoint.y = 0; // Ensure the point is on the same plane as the ground

        agent.SetDestination((spawnPoint != null ? spawnPoint.position : transform.position) + randomPoint);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            MoveCircle();
        }
    }

    void SpawnDeer()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point not assigned!");
            return;
        }

        for (int i = 0; i < numberOfDeer; i++)
        {
            Vector3 spawnPosition = spawnPoint.position + Random.insideUnitSphere * circleRadius;
            spawnPosition.y = 0f; // Ensure deer spawn on the ground level

            GameObject deer = Instantiate(deerPrefab, spawnPosition, Quaternion.identity);
            DeerController deerController = deer.GetComponent<DeerController>();
            if (deerController != null)
            {
                deerController.maxDistance = circleRadius;
            }
            else
            {
                Debug.LogError("Deer Controller component not found on the deer prefab!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((spawnPoint != null ? spawnPoint.position : transform.position), circleRadius);
    }
}
