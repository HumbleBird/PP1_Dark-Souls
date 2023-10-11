using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationUI : UI_Base
{
    enum GameObjcts
    {
        Yes,
        No
    }
    CharacterCreationScreen m_CharacterCreationScreen;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjcts));

        GetObject((int)GameObjcts.Yes).BindEvent(() => 
        {
            Managers.Sound.Play("Sounds/UI/UI_Button_Select_02");
            DontDestroyOnLoad(Managers.Object.m_MyPlayer.gameObject);
            m_CharacterCreationScreen.m_FadeInOutScreenUI.FadeOut(() => {
                Managers.Object.m_MyPlayer.gameObject.SetActive(false);
                Managers.Scene.LoadScene(Define.Scene.Game);
            });
        });
        GetObject((int)GameObjcts.No).BindEvent(() => gameObject.SetActive(false));

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        return true;
    }
}
