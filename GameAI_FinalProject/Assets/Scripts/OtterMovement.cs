using UnityEngine;

public class OtterMovement : MonoBehaviour
{
    public Transform waypointsParent; // Assign the parent GameObject containing waypoints in the Inspector
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 2.0f;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        if (waypointsParent == null)
        {
            Debug.LogError("No waypoints parent assigned. Please assign waypoints parent in the Inspector.");
            return;
        }

        // Get all child waypoints from the parent
        waypoints = new Transform[waypointsParent.childCount];
        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }
    }

    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned. Please assign waypoints in the Inspector.");
            return;
        }

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate towards the current waypoint
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Check if the dolphin is close enough to the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
