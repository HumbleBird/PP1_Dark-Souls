using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRightPanel : UI_Base
{
    enum Texts
    {

    }

    enum Images
    {

    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        //BindObject(typeof(GameObjects));

        return true;
    }
}
