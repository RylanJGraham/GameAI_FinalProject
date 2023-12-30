using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public List<Camera> generalCameras = new List<Camera>();
    public List<Camera> wolfCameras = new List<Camera>();
    public TMP_Dropdown dropdownMenu;

    private int currentWolfIndex = 0;

    private void Start()
    {
        PopulateDropdown();
        ActivateFirstCamera();
    }

    private void PopulateDropdown()
    {
        dropdownMenu.ClearOptions();

        List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();

        foreach (Camera cam in generalCameras)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(cam.name);
            dropdownOptions.Add(option);
        }

        dropdownOptions.Add(new TMP_Dropdown.OptionData("Wolves")); // Adding "Wolves" option
        dropdownMenu.AddOptions(dropdownOptions);
    }

    private void ActivateFirstCamera()
    {
        if (generalCameras.Count > 0)
        {
            foreach (Camera cam in generalCameras)
            {
                cam.gameObject.SetActive(false);
            }
            generalCameras[0].gameObject.SetActive(true);
        }
    }

    public void ToggleWolfCameras()
    {
        // Check if "Wolves" option is selected in the dropdown
        if (dropdownMenu.value == generalCameras.Count)
        {
            currentWolfIndex = (currentWolfIndex + 1) % wolfCameras.Count;
            SwitchToCurrentWolfCamera();
        }
    }

    private void Update()
    {
        if (dropdownMenu.value == generalCameras.Count) // Checking if "Wolves" option is selected
        {
            // Toggle through wolf cameras when the "Wolves" option is selected
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ToggleNextWolfCamera();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TogglePreviousWolfCamera();
            }
        }
    }

    private void ToggleNextWolfCamera()
    {
        currentWolfIndex = (currentWolfIndex + 1) % wolfCameras.Count;
        SwitchToCurrentWolfCamera();
    }

    private void TogglePreviousWolfCamera()
    {
        currentWolfIndex = (currentWolfIndex - 1 + wolfCameras.Count) % wolfCameras.Count;
        SwitchToCurrentWolfCamera();
    }

    private void SwitchToCurrentWolfCamera()
    {
        foreach (Camera cam in wolfCameras)
        {
            cam.gameObject.SetActive(false);
        }
        wolfCameras[currentWolfIndex].gameObject.SetActive(true);
    }

    public void ChangeCamera(int index)
    {
        if (index >= 0 && index < generalCameras.Count)
        {
            foreach (Camera cam in generalCameras)
            {
                cam.gameObject.SetActive(false);
            }
            generalCameras[index].gameObject.SetActive(true);
        }
        else if (index == generalCameras.Count) // Wolf option selected
        {
            currentWolfIndex = 0; // Reset wolf camera index to the first one
            SwitchToCurrentWolfCamera();
        }
    }

    public void OnDropdownValueChanged()
    {
        int selectedOption = dropdownMenu.value;
        ChangeCamera(selectedOption);
    }
}
