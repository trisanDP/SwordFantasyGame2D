using UnityEngine;
using Unity.Cinemachine;
using System.Linq;

public class CameraManager : MonoBehaviour
{
    private CinemachineCamera[] cameras;
    private int currentCameraIndex = 0;

    void Start()
    {
        // Get all CinemachineVirtualCameras in the scene and add them to the array
        cameras = Object.FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
    }

    void Update()
    {
        // Check if 'C' key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Disable the current camera
            cameras[currentCameraIndex].enabled = false;

            // Increment the index to switch to the next camera
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Enable the new current camera
            cameras[currentCameraIndex].enabled = true;
        }
    }
}