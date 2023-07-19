using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipmentSlotUI : EquipmentSlotUI
{
    public bool rightHandSlot01;
    public bool rightHandSlot02;
    public bool leftHandSlot01;
    public bool leftHandSlot02;

    public override void SelectThisSlot()
    {
        if(rightHandSlot01)
        {
            uiManager.rightHandSlot01Selected = true;
        }
        else if (rightHandSlot02)
        {
            uiManager.rightHandSlot02Selected = true;

        }
        else if (leftHandSlot01)
        {
            uiManager.leftHandSlot01Selected = true;

        }
        else
        {
            uiManager.leftHandSlot02Selected = true;

        }
    }
}
