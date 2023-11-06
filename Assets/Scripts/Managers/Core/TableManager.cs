using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TableManager
{
    public Table_Camera_Shake   m_Camera_Shake       = new Table_Camera_Shake();
    public Table_Camera_Zoom    m_Camera_Zoom           = new Table_Camera_Zoom();
    public Table_Item_Armor     m_Item_Armor            = new Table_Item_Armor();
    public Table_Item_Weapon    m_Item_Weapon           = new Table_Item_Weapon();
    public Table_Item_Spell     m_Item_Spell             = new Table_Item_Spell();
    public Table_Item_Ring      m_Item_Ring         = new Table_Item_Ring();
    public Table_Item_Tool      m_Item_Tool              = new Table_Item_Tool();
    public Table_Stat            m_Stat             = new Table_Stat();
    public Table_StartClassStat m_StartClassStat = new Table_StartClassStat();
    public Table_Monster         m_Monster       = new Table_Monster();
    public Table_SaveData_Character m_SaveData_Character = new Table_SaveData_Character();
    public Table_SaveData_Inventory m_SaveData_Inventory = new Table_SaveData_Inventory();

    public void Init()
    {
#if UNITY_EDITOR
        m_Camera_Shake      .Init_CSV("Camera_Shake", 2, 0);
        m_Camera_Zoom       .Init_CSV("Camera_Zoom", 2, 0);
        m_Item_Armor        .Init_CSV("Armor", 2, 0);
        m_Item_Weapon       .Init_CSV("Weapon", 2, 0);
        m_Item_Spell        .Init_CSV("Spell", 2, 0);
        m_Item_Ring         .Init_CSV("Ring", 2, 0);
        m_Item_Tool         .Init_CSV("ToolItem", 2, 0);
        m_Stat              .Init_CSV("Stat", 2, 0);
        m_StartClassStat    .Init_CSV("StartClassStat", 2, 0);
        m_Monster           .Init_CSV("Monster", 2, 0);
        //m_SaveData_Character           .Init_CSV("SaveData_Character", 2, 0);
        //m_SaveData_Inventory.Init_CSV("SaveData_Inventory", 2, 0);
#endif
    }

    public void Save()
    {
        //m_SaveData_Character.Save_Binary("SaveData_Character");
        //m_SaveData_Inventory.Save_Binary("SaveData_Inventory");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    public void Clear()
    {
    }
}
