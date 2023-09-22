using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationUI : UI_Popup
{
    enum GameObjcts
    {
        Yes,
        No
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjcts));

        GetObject((int)GameObjcts.Yes).BindEvent(() => 
        {
            DontDestroyOnLoad(Managers.Object.m_MyPlayer.gameObject);
            Managers.Scene.LoadScene(Define.Scene.Game);
        });
        GetObject((int)GameObjcts.No).BindEvent(() => Managers.UI.ClosePopupUI()); ;

        return true;
    }


}
