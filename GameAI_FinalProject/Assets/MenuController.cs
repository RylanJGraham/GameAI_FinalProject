using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    void Start()
    {
        // Hide the menu initially
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the menu's visibility
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        // Toggle the menu's active state
        menuCanvas.SetActive(!menuCanvas.activeSelf);

        // Pause the game while the menu is active (optional)
        Time.timeScale = (menuCanvas.activeSelf) ? 0 : 1;
    }
}
