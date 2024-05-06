using System.Collections;
using UnityEngine;

public class TrolleyController : MonoBehaviour
{
    public float railSwitchDelay = 1f; // Time to wait after reaching the end of the rail before allowing rail switch
    public float moveSpeed = 2f; // Speed of the trolley movement
    public float rotationAmount = 90f; // Amount to rotate the trolley when turning
    public float rotationSpeed = 90f; // Speed of the trolley rotation
    public float turnDistance = 1f; // Distance to move forward before starting the turn

    private bool isMoving = true; // Flag to control trolley movement
    private bool hasStopped = false; // Flag to track if the trolley has stopped moving forward

    private void Update()
    {
        // Check if the up arrow key is pressed to move forward
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveForward();
        }

        // Check if the trolley can switch rails
        if (!isMoving && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            // if (Input.GetKeyDown(KeyCode.LeftArrow))
            //     StartCoroutine(RotateTrolley(-rotationAmount)); // Rotate left by rotationAmount degrees
            // else if (Input.GetKeyDown(KeyCode.RightArrow))
            //     StartCoroutine(RotateTrolley(rotationAmount)); // Rotate right by rotationAmount degrees
        }

        // Check if the trolley has stopped and allow user input to resume movement
        if (hasStopped && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            // If the user presses left or right arrow key, resume movement
            isMoving = true;
            hasStopped = false;
            MoveForward();
        }
    }

    private void MoveForward()
    {
        // Move the trolley forward while rotating slightly
        StartCoroutine(MoveForwardAndTurnRoutine());
    }

    private IEnumerator MoveForwardAndTurnRoutine()
    {
        float distanceMoved = 0f;

        while (distanceMoved < 40f)
        {
            // Move the trolley forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            distanceMoved += moveSpeed * Time.deltaTime;

            // Check if the trolley has moved enough distance to start turning
            if (distanceMoved >= turnDistance && !hasStopped)
            {
                // Stop the trolley
                isMoving = false;
                hasStopped = true;

                // Rotate the trolley
                float angleToRotate = rotationAmount * Time.deltaTime * rotationSpeed;
                transform.Rotate(Vector3.up, angleToRotate);
            }

            yield return null;
        }
    }

    public IEnumerator RotateTrolley(float angle)
    {
        // Gradually rotate the trolley to align with the new direction
        float t = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, angle, 0f);

        while (t < 1f)
        {
            t += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        // Allow the trolley to move forward again
        MoveForward();
    }
}
