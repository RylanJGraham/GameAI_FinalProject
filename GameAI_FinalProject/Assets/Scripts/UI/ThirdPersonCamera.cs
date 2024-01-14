using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform targetObject;
    public float distance = 5.0f; // Distance from the target
    public float height = 3.0f; // Height from the target
    public float rotationSpeed = 2.0f; // Speed of camera rotation around the target

    private float currentRotationAngle = 0.0f;

    void LateUpdate()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object not assigned to the ThirdPersonCamera script.");
            return;
        }

        // Calculate the desired position based on distance and height
        Vector3 desiredPosition = targetObject.position - (targetObject.forward * distance) + (Vector3.up * height);

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        // Calculate the rotation angle based on user input or other controls
        currentRotationAngle += Input.GetAxis("Horizontal") * rotationSpeed;

        // Apply rotation around the target
        Quaternion rotation = Quaternion.Euler(0, currentRotationAngle, 0);
        transform.LookAt(targetObject);
        transform.Translate(Vector3.forward * -distance); // Move the camera backward along its forward vector
        transform.position = targetObject.position + rotation * transform.forward * distance;
    }
}
