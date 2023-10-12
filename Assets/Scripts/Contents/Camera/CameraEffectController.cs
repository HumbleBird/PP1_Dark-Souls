using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraEffectController : MonoBehaviour
{
    public void Start()
    {
        Managers.Camera.m_CameraEffectController = this;
        InitShake();
    }
}
