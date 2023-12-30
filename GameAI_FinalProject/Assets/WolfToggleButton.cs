using UnityEngine;

public class WolfToggleButton : MonoBehaviour
{
    public CameraManager cameraManager; // Reference to your CameraManager script

    // Method to toggle the wolf cameras
    public void ToggleWolfCameras()
    {
        // Toggle between the first wolf camera and no wolf cameras active
        cameraManager.ToggleWolfCameras();
    }
}
