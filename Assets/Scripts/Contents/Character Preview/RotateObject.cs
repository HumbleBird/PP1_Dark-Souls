using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    PlayerControls playerControls;

    public float rotationAmount = 5;
    public float rotationSpeed = 20;

    Vector2 cameraInput;

    Vector3 currentRotation;
    Vector3 targetRoatation;

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void Start()
    {
        currentRotation = transform.eulerAngles;
        targetRoatation = transform.eulerAngles;
    }

    private void Update()
    {
        if(cameraInput.x > 0)
        {
            targetRoatation.y += rotationAmount;
        }
        else if (cameraInput.x < 0)
        {
            targetRoatation.y -= rotationAmount;
        }

        currentRotation = Vector3.Lerp(currentRotation, targetRoatation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = currentRotation;
    }
}
