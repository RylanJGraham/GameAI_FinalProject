using UnityEngine;
using UnityEngine.AI;

public class MouseMove : MonoBehaviour {
    private NavMeshAgent agent;
    public Camera selectedCamera; // Variable to store the specific camera

    void Start () {
        agent = GetComponent<NavMeshAgent> ();
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            RaycastHit hit;
            Ray camRay;

            if (selectedCamera != null) {
                // If a specific camera is selected, use it
                camRay = selectedCamera.ScreenPointToRay (Input.mousePosition);
            } else {
                // If no specific camera is selected, use the main camera
                camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
            }

            if (Physics.Raycast (camRay, out hit, 100)) {
                agent.destination = hit.point;
            }
        }
    }
}
