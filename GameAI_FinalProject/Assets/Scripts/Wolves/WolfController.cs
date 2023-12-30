using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WolfController : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public LayerMask deerLayer; // Select the deer layer from the Inspector

    private NavMeshAgent agent;
    public Camera wolfCamera;
    private Transform deer;
    private bool hasLockedOnDeer = false; // Flag to track if the wolf has locked onto a deer
    private float timer;

    private bool inAttackMode = false;
    private Vector3 circlePosition;
    private Quaternion startRotation;

    private bool showRays = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wolfCamera = GetComponentInChildren<Camera>(); // Assuming the camera is a child of the wolf
        timer = wanderTimer;

        // Register this wolf with the WolfManager
        WolfManager.instance.RegisterWolf(this);

        // Start wandering
        Wander();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !hasLockedOnDeer)
        {
            Wander();
            timer = wanderTimer;
        }

        if (!hasLockedOnDeer)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                showRays = !showRays; // Toggle ray visibility on "D" key press
            }
            if (showRays) // Check if showRays is true to draw rays
            {
                DetectDeer();
            }
        }
        else if (deer != null)
        {
            ChaseDeer();
        }

        if (hasLockedOnDeer && inAttackMode && WolfManager.instance.AllWolvesInHuntRange())
        {
            agent.isStopped = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, 0.1f);
        }
    }

    void Wander()
    {
        Vector3 randomPoint = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(randomPoint);
    }

    void DetectDeer()
    {
        int raysCount = 50; // Number of rays to cast within the camera's field of view
        float angleIncrement = wolfCamera.fieldOfView / raysCount;
        bool deerDetected = false;

        for (int i = 0; i < raysCount; i++)
        {
            Quaternion rayRotation = Quaternion.AngleAxis(-wolfCamera.fieldOfView / 2 + angleIncrement * i, wolfCamera.transform.up);
            Vector3 direction = rayRotation * wolfCamera.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(wolfCamera.transform.position, direction, out hit, 20f, deerLayer)) // Limit raycast to 20 units and specific layer
            {
                Debug.DrawRay(wolfCamera.transform.position, direction * hit.distance, Color.red); // Draw the ray in the Scene view for debugging

                Debug.Log("Deer Spotted!");
                deer = hit.collider.transform;
                hasLockedOnDeer = true; // Lock onto the first detected deer
                deerDetected = true;
                break; // Exit the loop once a deer is spotted
            }
            else
            {
                Debug.DrawRay(wolfCamera.transform.position, direction * 20f, Color.blue); // Draw rays even if no deer detected
            }
        }

        if (!deerDetected)
        {
            deer = null;
            hasLockedOnDeer = false;
        }

        if (hasLockedOnDeer && deer != null)
        {
            MoveToDeer(deer);
        }
    }

    public void MoveToDeer(Transform deerTransform)
    {
        deer = deerTransform;
        hasLockedOnDeer = true;
        inAttackMode = false;

        // Move to the hunting position around the deer
        Vector3 directionToDeer = (deerTransform.position - transform.position).normalized;
        circlePosition = deerTransform.position - directionToDeer * WolfManager.instance.huntRange;
        agent.SetDestination(circlePosition);
        startRotation = transform.rotation;
    }

    public void MoveInToAttack(Transform deerTransform)
    {
        inAttackMode = true;
        agent.SetDestination(deerTransform.position);
    }

    void ChaseDeer()
    {
        if (deer != null)
        {
            // If the wolf has locked onto a deer, trigger all wolves to move towards it
            WolfManager.instance.MoveAllWolvesToDeer(deer);
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }
}
