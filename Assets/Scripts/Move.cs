using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The following class should move this Transform to the right until it reaches
/// (3, 0, 0) whre it resets to (-3, 0, 0) the moves to the right again.
/// It should move at 1m per second, but does not.
/// Please correct the code to move smoothly at 1m/s.
/// </summary>
public class Move : MonoBehaviour
{
    private float speed = 1.0f; // 1 meter per second
    private float totalDistance = 6.0f; // 6 meters
    private float currentTime = 0.0f;

    void Update()
    {
        // Move the cube from left to right
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Update the timer
        currentTime += Time.deltaTime;
        Debug.Log("Time : " + currentTime);
        // Check if the cube has moved the total distance
        if (transform.position.x > totalDistance / 2)
        {
            // Reset the position to the left
            transform.position = new Vector3(-totalDistance / 2, 0, 0);

            // Reset the timer
            currentTime = 0.0f;
        }

        // Check if the total time has elapsed
        if (currentTime >= totalDistance / speed)
        {
            // Reset the position to the left
            transform.position = new Vector3(-totalDistance / 2, 0, 0);

            // Reset the timer
            currentTime = 0.0f;
        }
    }

}
