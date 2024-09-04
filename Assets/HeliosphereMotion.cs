using UnityEngine;

public class HeliosphereMotion : MonoBehaviour
{
    // Speed of rotation around the GameObject's local Y-axis (up)
    public float yRotationSpeed = 45.0f; // Degrees per second

    // Speed of rotation around the GameObject's local Z-axis (forward)
    public float zRotationSpeed = 30.0f; // Degrees per second

    void Update()
    {
        // Rotate around the local Y-axis (up)
        transform.Rotate(Vector3.up, yRotationSpeed * Time.deltaTime, Space.Self);

        // Rotate around the local Z-axis (forward)
        transform.Rotate(Vector3.forward, zRotationSpeed * Time.deltaTime, Space.Self);
    }
}
