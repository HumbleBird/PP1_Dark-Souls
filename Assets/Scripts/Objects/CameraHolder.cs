using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    #region Variable

    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTranform;
    private Transform myTranform;
    private Vector3 cameraTransformPosition;
    public LayerMask ignoreLayers;

    public LayerMask enviromentLayer;
    private Vector3 cameraFollwVelocity = Vector3.zero;

    public static CameraHolder singleton;

    public float lookSpeed = 0.1f;
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

    public float maximunLockOnDistance = 30f;

    //public List<Character> m_ListAvailableTarget = new List<Character>();
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
        //targetTransform = Managers.Object.myPlayer.gameObject.transform;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        enviromentLayer = LayerMask.NameToLayer("Obstacle");
    }
}
