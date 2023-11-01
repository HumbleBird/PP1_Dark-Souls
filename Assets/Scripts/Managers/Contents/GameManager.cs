using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameManager
{
    public Interactable m_Interactable;
    PlayerManager m_Player;

    public bool m_isNewGame = true;
    public bool isReSetting = false;

    DeadSouls m_goDeadSouls;

    // Inventory
    public int m_iInventoryCurrentSelectItemSlotNum = 0;
    public int m_iInventoryShowItemTapLeftNum = 0;
    public int m_iInventoryShowItemTapRightNum = 4;
    public int m_iInventoryCurrentSelectTapNum = 0;

    public void GameStart()
    {
        m_Player = Managers.Object.m_MyPlayer;

        m_Player.StartGame();
    }

    public void PlayAction(Action action)
    {
        action.Invoke();
    }

    public void EntryNewArea(string name)
    {
        // UI로 화면 뛰우기
        m_Player.m_GameSceneUI.m_AreaUI.ShowNewAreaName(name);

    }

    public IEnumerator PlayerDead()
    {

        // 유다이 Popup (+ 사운드)
        m_Player.m_GameSceneUI.ShowYouDied();

        // 페이드 아웃
        yield return new WaitForSeconds(5);

        m_Player.m_GameSceneUI.m_FadeInOutScreenUI.FadeOut();

        // 페이드 아웃 유지 시간
        yield return new WaitForSeconds(3);
        isReSetting = true;

        // 페이드 아웃이 전부 끝나면 3초간 대기
        // 이후에는 여기에 서버 데이터를 받을 것.

        // 소울이 이미 있다면
        if(m_goDeadSouls != null)
            Managers.Resource.Destroy(m_goDeadSouls.gameObject);

        // 소울을 남김
        m_goDeadSouls = Managers.Resource.Instantiate("Objects/Interact Object/Dead Soul").GetComponent<DeadSouls>();
        m_goDeadSouls.transform.position = m_Player.transform.position;
        m_goDeadSouls.m_iSoulsCount = m_Player.playerStatsManager.currentSoulCount;

        // UI 업데이트
        m_Player.m_GameSceneUI.RefreshUI(Define.E_StatUI.All);

        //모든 캐릭터 초기화
        foreach (GameObject go in Managers.Object._objects)
        {
            CharacterManager character = go.GetComponent<CharacterManager>();

            character.InitCharacterManager();
        }

        yield return new WaitForSeconds(1);
        isReSetting = false;

        // 페이드 인
        m_Player.m_GameSceneUI.m_FadeInOutScreenUI.FadeIn();

        yield break;
    }

    public void PlayerisStop(bool isStop = true)
    {
        Managers.Camera.m_Camera.m_isCanRotate = !isStop;
        Managers.Object.m_MyPlayer.playerLocomotionManager.m_isCanMove = !isStop;
    }

    public Item MakeItem(E_ItemType type, int id)
    {
        Item item = null;
        switch (type)
        {
            case E_ItemType.Tool:
                item = new ToolItem(id);
                break;
            case E_ItemType.ReinforcedMaterial:
                break;
            case E_ItemType.Valuables:
                break;
            case E_ItemType.Magic:
                SpellItem spell = new SpellItem(id);
                if (spell.m_eSpellType == E_SpellType.Faith)
                    item = (HealingSpell)spell;
                else if (spell.m_eSpellType == E_SpellType.Pyro)
                    item = (ProjectileSpell)spell;
                break;
            case E_ItemType.MeleeWeapon:
            case E_ItemType.RangeWeapon:
            case E_ItemType.Catalyst:
            case E_ItemType.Shield:
                item = new WeaponItem(id);
                break;
            case E_ItemType.Helmet:
                item = new HelmEquipmentItem(id);
                break;
            case E_ItemType.ChestArmor:
                item = new TorsoEquipmentItem(id);
                break;
            case E_ItemType.Gauntlets:
                item = new GantletsEquipmentItem(id);
                break;
            case E_ItemType.Leggings:
                item = new LeggingsEquipmentItem(id);
                break;
            case E_ItemType.Ammo:
                break;
            case E_ItemType.Ring:
                break;
            case E_ItemType.Pledge:
                break;
            default:
                break;
        }

        return item;
    }
}
