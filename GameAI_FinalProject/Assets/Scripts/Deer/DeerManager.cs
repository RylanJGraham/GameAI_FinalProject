using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DeerManager : MonoBehaviour
{
    public GameObject deerPrefab; // Reference to the deer prefab
    public int numberOfDeer = 10; // Number of deer to spawn
    public float movementSpeed = 3f; // Speed at which the zone moves
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Prevent rotation for constant movement

        StartCoroutine(SpawnDeer());
        StartCoroutine(MoveZone());
    }

    IEnumerator SpawnDeer()
    {
        for (int i = 0; i < numberOfDeer; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 10f; // Adjust the radius as needed
            spawnPosition.y = 0f; // Ensure deer spawn on the ground level

            GameObject deer = Instantiate(deerPrefab, spawnPosition, Quaternion.identity);
            DeerController deerController = deer.GetComponent<DeerController>();
            if (deerController != null)
            {
                deerController.Initialize(this);
            }
            yield return null;
        }
    }

    IEnumerator MoveZone()
    {
        while (true)
        {
            Vector3 randomDestination = transform.position + Random.insideUnitSphere * 20f; // Adjust the radius as needed
            randomDestination.y = 0f; // Ensure the zone moves on the ground level

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDestination, out hit, 20f, NavMesh.AllAreas); // Adjust sample distance as needed
            agent.SetDestination(hit.position);

            yield return new WaitForSeconds(Random.Range(3f, 6f)); // Adjust movement frequency
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 20f); // Adjust the radius to match the zone
    }
}
