using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    #region Variable

    InputHandler inputHandler;

    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTranform;
    private Transform myTranform;
    private Vector3 cameraTransformPosition;
    public LayerMask ignoreLayers;

    public LayerMask enviromentLayer;
    private Vector3 cameraFollwVelocity = Vector3.zero;

    public static CameraHandler singleton;

    public float lookSpeed = 0.01f;
    public float follwSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float targetPostion;

    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;

    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffSet = 0.2f;
    public float minimumCollisionOffSet = 0.2f;
    public float m_fLockedPivotPosition = 2.25f;
    public float m_fUnlockedPivotPosition = 1.65f;

    public float maximumLockOnDistance = 30f;

    public List<CharacterManager> m_ListAvailableTarget = new List<CharacterManager>();
        
    public Transform m_trNearestLockOnTarget;
    public Transform m_trCurrentLockOnTarget;
    public Transform m_trleftLockTarget;
    public Transform m_trRightLockTarget;

    #endregion

    private void Awake()
    {
        singleton = this;
        myTranform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputHandler = FindObjectOfType<InputHandler>();
    }

    public void FollwTarget(float delta)
    {
        Vector3 targetPositoin = Vector3.SmoothDamp(myTranform.position, targetTransform.position, ref cameraFollwVelocity, delta / follwSpeed);
        myTranform.position = targetPositoin;

        HandleCameraCollision(delta);
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        if (inputHandler.lockOnFlag == false && m_trCurrentLockOnTarget == null)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTranform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTranform.localRotation = targetRotation;
        }
        else
        {
            float velocity = 0;

            Vector3 dir = m_trCurrentLockOnTarget.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            dir = m_trCurrentLockOnTarget.position - cameraPivotTranform.position;
            dir.Normalize();

            targetRotation = Quaternion.LookRotation(dir);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivotTranform.localEulerAngles = eulerAngle;
        }


    }

    private void HandleCameraCollision(float delta)
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

        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPostion, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;
    }

    public void HandleLockOn()
    {
        float shortDistance = Mathf.Infinity;
        float shorttestDistanceOfLeftTarget = Mathf.Infinity;
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

                if(character.transform.root != targetTransform.root && 
                    viewableAngle > -50 && viewableAngle < 50
                     && distanceFromTarget <= maximumLockOnDistance)
                {
                    m_ListAvailableTarget.Add(character);
                }
            }
        }

        for (int k = 0; k < m_ListAvailableTarget.Count; k++)
        {
            float distanceFromTarget = Vector3.Distance(targetTransform.position, m_ListAvailableTarget[k].transform.position);
            
            if(distanceFromTarget < shortDistance)
            {
                shortDistance = distanceFromTarget;
                m_trNearestLockOnTarget = m_ListAvailableTarget[k].lockOnTransform;
            }

            if(inputHandler.lockOnFlag)
            {
                Vector3 relativeEnemyPosition = m_trCurrentLockOnTarget.InverseTransformPoint(m_ListAvailableTarget[k].transform.position);
                var distanceFromLeftTarget = m_trCurrentLockOnTarget.transform.position.x - m_ListAvailableTarget[k].transform.position.x;
                var distanceFromRightTarget = m_trCurrentLockOnTarget.transform.position.x + m_ListAvailableTarget[k].transform.position.x;
            
                if(relativeEnemyPosition.x > 0.00 && distanceFromLeftTarget < shorttestDistanceOfLeftTarget)
                {
                    shorttestDistanceOfLeftTarget = distanceFromLeftTarget;
                    m_trleftLockTarget = m_ListAvailableTarget[k].lockOnTransform;
                }

                if(relativeEnemyPosition.x < 0.00 && distanceFromRightTarget < shorttestDistanceOfRightTarget)
                {
                    shorttestDistanceOfRightTarget = distanceFromRightTarget;
                    m_trRightLockTarget = m_ListAvailableTarget[k].lockOnTransform;
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
