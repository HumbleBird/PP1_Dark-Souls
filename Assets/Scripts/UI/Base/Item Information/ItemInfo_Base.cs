using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo_Base : UI_Base
{
    protected enum Texts
    {

    }


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        return true;
    }

    public virtual void ShowItemInfo(Item item)
    {

    }
}