using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Speed of camera movement
    public float rotationSpeed = 5.0f; // Speed of camera rotation
    public float riseSpeed = 10.0f; // Speed of camera rising
    public float descendSpeed = 10.0f; // Speed of camera descending

    private float pitch = 0f;
    private float yaw = 0f;

    private void Update()
    {
        // Camera rotation based on mouse movement
        yaw += rotationSpeed * Input.GetAxis("Mouse X");
        pitch -= rotationSpeed * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        // Camera movement based on arrow keys (or any other keys for movement)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Camera rising (Shift key)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 riseVector = Vector3.up * riseSpeed * Time.deltaTime;
            transform.Translate(riseVector);
        }

        // Camera descending (Ctrl key)
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 descendVector = Vector3.down * descendSpeed * Time.deltaTime;
            transform.Translate(descendVector);
        }
    }
}
