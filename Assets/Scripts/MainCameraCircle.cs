using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraCircle : MonoBehaviour
{
    public float radius = 5f;           // Distance from the center of the circle
    public float speed = 1f;            // Speed of rotation
    public Vector3 centerPoint = Vector3.zero; // Center of the circle
    public float startY = 300f;          // Starting y position
    public float targetY = 7.4f;        // Target y position
    public float descentDuration = 10f; // Duration in seconds to reach the target y position

    private float angle = 0f;           // Current angle in radians
    private float timeElapsed = 0f;     // Time elapsed since the start

    void Update()
    {
        // Increment the angle based on the speed and the time passed
        angle += speed * Time.deltaTime;
        timeElapsed += Time.deltaTime;

        // Calculate the new position around the circle
        float x = centerPoint.x + radius * Mathf.Cos(angle);
        float z = centerPoint.z + radius * Mathf.Sin(angle);

        // Interpolate the y position from startY to targetY over descentDuration seconds
        float y = Mathf.Lerp(startY, targetY, timeElapsed / descentDuration);
        y = Mathf.Clamp(y, targetY, startY); // Clamp to ensure it doesn't go below targetY after reaching it

        Vector3 newPosition = new Vector3(x, y, z);

        // Move the camera to the new position
        transform.position = newPosition;

        // Always look at the center point
        transform.LookAt(centerPoint);
    }
}
