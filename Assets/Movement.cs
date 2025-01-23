using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;    
    public float lookSpeedX = 2f;     
    public float lookSpeedY = 2f;       
    public Transform playerBody;        

    private Camera playerCamera;       
    private float rotationX = 0f;    

    public float bobSpeed = 5f;        
    public float bobAmount = 0.05f;     

    private Vector3 initialCameraPosition;  

    void Start()
    {
        playerCamera = Camera.main;     
        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = false;         

        initialCameraPosition = playerCamera.transform.localPosition; 
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;  
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;  

        playerBody.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        float moveDirectionZ = Input.GetAxis("Vertical");
        float moveDirectionX = Input.GetAxis("Horizontal"); 

        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        if (move.magnitude > 0) // Only bob when the player is moving in any direction
        {
            float bob = Mathf.Sin(Time.time * bobSpeed) * bobAmount;

            playerCamera.transform.localPosition = new Vector3(initialCameraPosition.x, initialCameraPosition.y + bob, initialCameraPosition.z);
        }
        else
        {
            playerCamera.transform.localPosition = initialCameraPosition;
        }
    }
}
