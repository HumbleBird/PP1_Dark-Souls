using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Transform cameraObject;
    InputHandler inputHandler;
    Vector3 movedirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimatorHandler animatorHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animatorHandler.Initialize();
    }

    public void Update()
    {
        float delta = Time.deltaTime;

        inputHandler.TickInput(delta);

        movedirection = cameraObject.forward * inputHandler.vertical;
        movedirection += cameraObject.right * inputHandler.horizontal;
        movedirection.Normalize();
        movedirection.y = 0;

        float speed = movementSpeed;
        movedirection *= speed;

        Vector3 projecteedVelocity = Vector3.ProjectOnPlane(movedirection, normalVector);
        rigidbody.velocity = projecteedVelocity;


        animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, false);

        if (animatorHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }

    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * Time.deltaTime);

        myTransform.rotation = targetRotation;
    }

    #endregion
}
