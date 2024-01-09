using UnityEngine;

public class DebugMenuController : MonoBehaviour
{
    public GameObject gizmoObject; // Assign your Gizmo GameObject in the Inspector

    // This function will be called by the button click event
    public void ToggleGizmos()
    {
        // Toggle the visibility of the Gizmo GameObject
        if (gizmoObject != null)
        {
            gizmoObject.SetActive(!gizmoObject.activeSelf);
        }
    }
}
