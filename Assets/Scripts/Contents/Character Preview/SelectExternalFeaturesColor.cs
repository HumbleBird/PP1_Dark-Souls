using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class SelectExternalFeaturesColor : MonoBehaviour
{
    PlayerManager player;

    [Header("Color Values")]
    public float redAmount;
    public float greenAmount;
    public float blueAmount;
    public float alphaAmount;

    [Header("Color Sliders")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    private Color currentHairColor;

    // skin mesh renderer�κ����� material�� ��´�. ���׸��� �Ӽ����� �����Ѵ�.
    public List<SkinnedMeshRenderer> m_HairColorRendererList = new List<SkinnedMeshRenderer>();
    public List<SkinnedMeshRenderer> m_FacialMaskColorRendererList = new List<SkinnedMeshRenderer>();
    public List<SkinnedMeshRenderer> m_SkinColorRendererList = new List<SkinnedMeshRenderer>();

    private void Start()
    {

    }

    public void SetInfo()
    {
        // �÷��̾� ��ⷯ�� ��� �����ϰ� �� �Ŀ� ȣ���ؾ� ��.

        List<GameObject> list = new List<GameObject>();

        list = player.playerEquipmentManager.m_AllGenderPartsModelChanger[All_GenderItemPartsType.Hair].equipments;

        foreach (GameObject item in list)
        {
            SkinnedMeshRenderer skin = item.GetComponent<SkinnedMeshRenderer>();
            m_HairColorRendererList.Add(skin);
        }
    }

    public void UpdateSliders()
    {
        redAmount = redSlider.value;
        greenAmount = greenSlider.value;
        blueAmount = blueSlider.value;
        SetPartsColor();
    }

    public void SetPartsColor()
    {
        currentHairColor = new Color(redAmount, greenAmount, blueAmount);

        for (int i = 0; i < m_HairColorRendererList.Count; i++)
        {
            m_HairColorRendererList[i].material.SetColor("_Color_Hair", currentHairColor);
        }
    }
}

