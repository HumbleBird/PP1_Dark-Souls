using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDUI : UI_Base
{
    enum GameObjects
    {
        StatBars,
        Crosshair,
        BossHealthBar,
    }

    enum Texts
    {
        SoulCount,

    }

    public QuickSlotsUI quickSlotsUI;
    public TextMeshProUGUI m_textSoulCount;
    public GameObject m_goStatBars;
    public GameObject m_goCrosshair;
    public GameObject m_goBossHealthBar;
    public ItemStatWindowUI itemStatWindowUI;
    public EquipmentWindowUI equipmentWindowUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        m_textSoulCount = GetText((int)Texts.SoulCount);
        m_goStatBars = GetObject((int)GameObjects.StatBars);
        m_goCrosshair = GetObject((int)GameObjects.Crosshair);
        m_goBossHealthBar = GetObject((int)GameObjects.BossHealthBar);

        quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
        return true;
    }


}
