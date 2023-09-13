using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationRightPannelUI : UI_Base
{
    CharacterCreationScreen m_CharacterCreationScreen;

    enum GameObjects
    {
    }

    enum Buttons
    {
    }



    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();

        //BindButton(typeof(Buttons));
        //BindObject(typeof(GameObjects));

        return true;
    }

    public void SetInfo()
    {

    }

    public void AllPannelButonInteractable(bool isTrue)
    {
    }

    public void PostSetInfo()
    {
    }
}
