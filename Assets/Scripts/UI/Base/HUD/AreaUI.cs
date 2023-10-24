using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaUI : UI_Base
{
    enum Texts
    {
        AreaText
    }

    public TextMeshProUGUI m_AreaText;

    Animator m_Animator;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));


        m_AreaText = GetText((int)Texts.AreaText);

        m_Animator = GetComponent<Animator>();

        return true;
    }

    public void ShowNewAreaName(string areaName)
    {
        m_AreaText.text = areaName;
        m_Animator.Play("NewAreaEntry");
    }

    public void NewAreSoundPlay()
    {
        Managers.Sound.Play("Area/NewAreaEntry");
    }
}
