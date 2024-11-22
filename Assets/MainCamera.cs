using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float panSpeed = 20f; // Speed of camera panning
    public float panBorderThickness = 10f; // Distance from screen edge to start panning

    void Update()
    {
        // Check if right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseMovement = new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y"));

            // Adjust the movement speed based on panSpeed and deltaTime
            mouseMovement *= panSpeed * Time.deltaTime;

            // Apply the movement to the camera's position
            transform.Translate(mouseMovement, Space.World);
        }
    }
}
