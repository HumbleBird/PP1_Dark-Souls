 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonfireLitPopupUI : MonoBehaviour
{
    public Animator m_Animator;

    public void Start()
    {
        m_Animator = GetComponent<Animator>();

        m_Animator.Play("BonFireLit");

        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(2f);
        Managers.Resource.Destroy(gameObject);
    }
}
