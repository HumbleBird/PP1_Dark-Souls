using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadEquipmentSlotUI : EquipmentSlotUI
{
    

    public override void SelectThisSlot()
    {
        uiManager.headEquipmentSlotSelected = true;

    }
}
