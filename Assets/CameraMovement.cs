using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;  // The player object to follow
    public float rotationSpeed = 5f;  // Speed of the camera's rotation

    private Vector3 offset;  // The offset from the player

    void Start()
    {
        // Set the initial offset slightly above the player (height only)
        offset = new Vector3(0, 1.5f, 0);  // Adjust the height above the player
    }

    void Update()
    {
        // Get mouse input for rotating the camera horizontally
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

        // Apply rotation to the offset vector using the mouse input
        offset = Quaternion.Euler(0, mouseX, 0) * offset;

        // Set the camera's position by adding the offset to the player's position
        transform.position = player.position + offset;

        // Make the camera always look at the player
        transform.LookAt(player);
    }
}
