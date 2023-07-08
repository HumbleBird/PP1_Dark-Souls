using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    #region Variable

    InputHandler inputHandler;
    PlayerManager playerManager;

    public Transform targetTransform;  // 플레이어를 따라감
    public Transform targetTransformWhileAiming; // 플레이어를 따라감 aim 중일 때 한정
    public Transform cameraTransform;
    public Camera cameraObject;
    public Transform cameraPivotTranform;

    private Vector3 cameraTransformPosition;
    private Vector3 cameraFollwVelocity = Vector3.zero;

    public LayerMask ignoreLayers;
    public LayerMask enviromentLayer;


    public static CameraHandler singleton;

    public float leftAndRightLookSpeed = 250f;
    public float leftAndRightAimingLookSpeed = 25f;
    public float follwSpeed = 1f;
    public float unAndDownLookSpeed = 250f;
    public float unAndDownAimingLookSpeed = 25f;

    private float targetPostion;
    private float defaultPosition;

    private float leftAndRightAngle;
    private float upAndDownAngle;

    public float minimumLockUpAngle = -35;
    public float maximumLockUpAngle = 35;

    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffSet = 0.2f;
    public float minimumCollisionOffSet = 0.2f;
    public float m_fLockedPivotPosition = 2.25f;
    public float m_fUnlockedPivotPosition = 1.65f;

    public float maximumLockOnDistance = 30f;

    public CharacterManager m_trCurrentLockOnTarget;

    List<CharacterManager> m_ListAvailableTarget = new List<CharacterManager>();
        
    public CharacterManager m_trNearestLockOnTarget;
    public CharacterManager m_trleftLockTarget;
    public CharacterManager m_trRightLockTarget;

    #endregion

    private void Awake()
    {
        singleton = this;
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10 |  1 << 12);
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputHandler = FindObjectOfType<InputHandler>();
        playerManager = FindObjectOfType<PlayerManager>();
        cameraObject = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        enviromentLayer = LayerMask.NameToLayer("Enviroment");
    }

    // Follow the Player
    public void FollowTarget()
    {
        if(playerManager.isAiming)
        {
            Vector3 targetPositoin = Vector3.SmoothDamp(transform.position, targetTransformWhileAiming.position, ref cameraFollwVelocity, Time.deltaTime * follwSpeed);
            transform.position = targetPositoin;

        }
        else
        {
            Vector3 targetPositoin = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollwVelocity, Time.deltaTime * follwSpeed);
            transform.position = targetPositoin;

        }

        HandleCameraCollision();

    }

    // Rotate the  Camera
    public void HandleCameraRotation()
    {
        if(inputHandler.lockOnFlag && m_trCurrentLockOnTarget != null)
        {
            HandleLockedCameraRotation();
        }
        else if (playerManager.isAiming)
        {
            HandleAimedCameraRotation();
        }
        else
        {
            HandleStandardCameraRotation();
        }
    }

    public void HandleStandardCameraRotation()
    {
        leftAndRightAngle += inputHandler.mouseX * leftAndRightLookSpeed * Time.deltaTime;
        upAndDownAngle -= inputHandler.mouseY * unAndDownLookSpeed * Time.deltaTime;
        upAndDownAngle = Mathf.Clamp(upAndDownAngle, minimumLockUpAngle, maximumLockUpAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = leftAndRightAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = upAndDownAngle;

        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTranform.localRotation = targetRotation;

    }

    private void HandleLockedCameraRotation()
    {
        Vector3 dir = m_trCurrentLockOnTarget.transform.position - transform.position;
        dir.Normalize();
        dir.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = targetRotation;

        dir = m_trCurrentLockOnTarget.transform.position - cameraPivotTranform.position;
        dir.Normalize();

        targetRotation = Quaternion.LookRotation(dir);
        Vector3 eulerAngle = targetRotation.eulerAngles;
        eulerAngle.y = 0;
        cameraPivotTranform.localEulerAngles = eulerAngle;
    }

    private void HandleAimedCameraRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        cameraPivotTranform.rotation = Quaternion.Euler(0, 0, 0);

        Quaternion targetRotationX;
        Quaternion targetRotationY;

        Vector3 cameraRotationX = Vector3.zero;
        Vector3 cameraRotationY = Vector3.zero;

        leftAndRightAngle += (inputHandler.mouseX * leftAndRightAimingLookSpeed) * Time.deltaTime;
        upAndDownAngle -= (inputHandler.mouseY * unAndDownAimingLookSpeed) * Time.deltaTime;

        cameraRotationY.y = leftAndRightAngle;
        targetRotationY = Quaternion.Euler(cameraRotationY);
        targetRotationY = Quaternion.Slerp(transform.rotation, targetRotationY, 1);
        transform.localRotation = targetRotationY;

        cameraRotationX.x = upAndDownAngle;
        targetRotationX = Quaternion.Euler(cameraRotationX);
        targetRotationX = Quaternion.Slerp(cameraTransform.localRotation, targetRotationX, 1);
        cameraTransform.localRotation = targetRotationX;
    }

    // Handle Collision
    private void HandleCameraCollision()
    {
        targetPostion = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTranform.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivotTranform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPostion)
            , ignoreLayers))
        {
            float dis = Vector3.Distance(cameraPivotTranform.position, hit.point);
            targetPostion = -(dis - cameraCollisionOffSet);
        }

        if (Mathf.Abs(targetPostion) < minimumCollisionOffSet)
        {
            targetPostion = -minimumCollisionOffSet;
        }

        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPostion, Time.deltaTime / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;
    }


    // Handle Lock On
    public void HandleLockOn()
    {
        float shortDistance = Mathf.Infinity;
        float shorttestDistanceOfLeftTarget = -Mathf.Infinity;
        float shorttestDistanceOfRightTarget = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager character = colliders[i].GetComponent<CharacterManager>();

            if(character != null)
            {
                Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.position, character.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);
                RaycastHit hit;

                if(character.transform.root != targetTransform.root && 
                    viewableAngle > -50 && viewableAngle < 50
                     && distanceFromTarget <= maximumLockOnDistance)
                {
                    if(Physics.Linecast(playerManager.lockOnTransform.position ,character.lockOnTransform.position, out hit))
                    {
                        Debug.DrawLine(playerManager.lockOnTransform.position, character.lockOnTransform.position);

                        if(hit.transform.gameObject.layer == enviromentLayer)
                        {

                        }
                        else
                        {
                            m_ListAvailableTarget.Add(character);
                        }
                    }
                }
            }
        }

        for (int k = 0; k < m_ListAvailableTarget.Count; k++)
        {
            float distanceFromTarget = Vector3.Distance(targetTransform.position, m_ListAvailableTarget[k].transform.position);
            
            if(distanceFromTarget < shortDistance)
            {
                shortDistance = distanceFromTarget;
                m_trNearestLockOnTarget = m_ListAvailableTarget[k];
            }

            if(inputHandler.lockOnFlag)
            {
                //Vector3 relativeEnemyPosition = m_trCurrentLockOnTarget.transform.InverseTransformPoint(m_ListAvailableTarget[k].transform.position);
                //var distanceFromLeftTarget = m_trCurrentLockOnTarget.transform.position.x - m_ListAvailableTarget[k].transform.position.x;
                //var distanceFromRightTarget = m_trCurrentLockOnTarget.transform.position.x + m_ListAvailableTarget[k].transform.position.x;
                Vector3 relativeEnemyPosition = inputHandler.transform.InverseTransformPoint(m_ListAvailableTarget[k].transform.position);
                var distanceFromLeftTarget = relativeEnemyPosition.x;
                var distanceFromRightTarget = relativeEnemyPosition.x;

                if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shorttestDistanceOfLeftTarget
                    && m_ListAvailableTarget[k] != m_trCurrentLockOnTarget)
                {
                    shorttestDistanceOfLeftTarget = distanceFromLeftTarget;
                    m_trleftLockTarget = m_ListAvailableTarget[k];
                }

                if(relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shorttestDistanceOfRightTarget
                    && m_ListAvailableTarget[k] != m_trCurrentLockOnTarget)
                {
                    shorttestDistanceOfRightTarget = distanceFromRightTarget;
                    m_trRightLockTarget = m_ListAvailableTarget[k];
                }
            }
        }
    }

    public void ClearLockOnTargets()
    {
        m_ListAvailableTarget.Clear();
        m_trNearestLockOnTarget = null;
        m_trCurrentLockOnTarget = null;
    }

    public void SetCameraHeight()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 newLockedPosition = new Vector3(0, m_fLockedPivotPosition);
        Vector3 newUnLockedPosition = new Vector3(0, m_fUnlockedPivotPosition);

        if(m_trCurrentLockOnTarget != null)
        {
            cameraPivotTranform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTranform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
        }
        else
        {
            cameraPivotTranform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTranform.transform.localPosition, newUnLockedPosition, ref velocity, Time.deltaTime);
        }
    }
}
