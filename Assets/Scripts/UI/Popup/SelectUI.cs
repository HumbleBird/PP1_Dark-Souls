using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectUI : UI_Popup
{
    enum GameObjects
    {
        Equipment,
        Inventory,
        Status,
        Message,
        System,
    }

    enum Images
    {
        EquipmentSelectIcon,
        InventorySelectIcon,
        StatusSelectIcon,
        MessageSelectIcon,
        SystemSelectIcon,
    }

    public TextMeshProUGUI m_TopSelectText;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));

        m_TopSelectText.text = "";

        // Equipment
        {
            GameObject go = GetObject((int)GameObjects.Equipment);
            Image img = GetImage((int)Images.EquipmentSelectIcon);

            ImageTrigger(go, img, SelectEquipment);
        }

        // Inventory
        {
            GameObject go = GetObject((int)GameObjects.Inventory);
            Image img = GetImage((int)Images.InventorySelectIcon);

            ImageTrigger(go, img, SelectInventory);
        }

        // Status
        {
            GameObject go = GetObject((int)GameObjects.Status);
            Image img = GetImage((int)Images.StatusSelectIcon);

            ImageTrigger(go, img, SelectStatus);
        }

        // Message
        {
            GameObject go = GetObject((int)GameObjects.Message);
            Image img = GetImage((int)Images.MessageSelectIcon);

            ImageTrigger(go, img, SelectMessage);
        }

        // Setting
        {
            GameObject go = GetObject((int)GameObjects.System);
            Image img = GetImage((int)Images.SystemSelectIcon);

            ImageTrigger(go, img, SelectSystem);
        }


        return true;
    }

    void SelectEquipment()
    {
        Managers.GameUI.m_EquipmentUI = Managers.GameUI.ShowPopupUI<EquipmentUI>();
    }

    void SelectInventory()
    {
        Managers.GameUI.ShowPopupUI<InventoryUI>();
    }

    void SelectStatus()
    {
        Managers.GameUI.ShowPopupUI<StatusUI>();

    }

    void SelectMessage()
    {

    }

    void SelectSystem()
    {
        // 저장 여부 확인
        Managers.GameUI.ShowPopupUI<SystemUI>();

    }

    void ImageTrigger(GameObject go, Image image, Action action)
    {
        go.UIEventTrigger(EventTriggerType.PointerEnter, () => { image.enabled = true; });

        image.gameObject.UIEventTrigger(EventTriggerType.PointerEnter, () => 
        { 
            Managers.Sound.Play("UI/Popup_OrderButtonSelect");
            m_TopSelectText.gameObject.SetActive(true);
            m_TopSelectText.text = go.name;
        });

        image.gameObject.UIEventTrigger(EventTriggerType.PointerExit, () =>
        {
            m_TopSelectText.gameObject.SetActive(false);
            image.enabled = false;
        });

        image.gameObject.BindEvent(() => { Managers.Sound.Play("UI/Popup_ButtonShow"); Managers.GameUI.ClosePopupUI(); action();  });
        //image.gameObject.UIEventTrigger(EventTriggerType.Select, () =>
        //{ Managers.Game.PlayAction( () => { Managers.Sound.Play("UI/Popup_ButtonShow"); Managers.GameUI.ClosePopupUI(); action(); } ); });
    }
}
