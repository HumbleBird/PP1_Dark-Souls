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
    public bool m_Develing = true;

    DeadSouls m_goDeadSouls;
    public int m_iPlayerDeadSoul = 0;

    // Inventory
    public int m_iInventoryCurrentSelectItemSlotNum = 0;
    public int m_iInventoryShowItemTapLeftNum = 0;
    public int m_iInventoryShowItemTapRightNum = 4;
    public int m_iInventoryCurrentSelectTapNum = 0;

    // Boss
    public EnemyBossManager m_Boss;

    // Area
    public Dictionary<string, Area> m_DicAreas = new Dictionary<string, Area>();

    public void GameStart()
    {
        if(Managers.Object.m_MyPlayer == null)
        {
            GameObject player =  Managers.Resource.Instantiate("Player/Player (Game)");
            PlayerManager pm =player.GetComponent<PlayerManager>();
            m_Player = pm;

        }
        else
        {
            m_Player = Managers.Object.m_MyPlayer;
        }

        m_Player.StartGame();
    }

    public void PlayAction(Action action)
    {
        action.Invoke();
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

        // Dead Souls
        if(m_goDeadSouls != null)
            Managers.Resource.Destroy(m_goDeadSouls.gameObject);
        m_goDeadSouls = Managers.Resource.Instantiate("Objects/Interact Object/Dead Soul").GetComponent<DeadSouls>();
        m_goDeadSouls.transform.position = m_Player.transform.position;
        m_goDeadSouls.m_iSoulsCount = m_iPlayerDeadSoul;
        m_iPlayerDeadSoul = 0;

        // 지역 초기화
        foreach (Area area in m_DicAreas.Values)
        {
            area.Clear();
        }

        // UI 업데이트
        m_Player.m_GameSceneUI.RefreshUI(Define.E_StatUI.All);
        Managers.GameUI.CloseAllPopupUI();
        Managers.GameUI.m_GameSceneUI.m_BossHealthBar.Clear();

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
                item = ToolItemSegmantation(id);
                break;
            case E_ItemType.ReinforcedMaterial:
                break;
            case E_ItemType.Valuables:
                break;
            case E_ItemType.Magic:
                item = new SpellItem(id);
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
                item = new AmmoItem(id);
                break;
            case E_ItemType.Ring:
                item = new RingItem(id);
                break;
            case E_ItemType.Pledge:
                break;
            default:
                break;
        }

        return item;
    }

    ToolItem ToolItemSegmantation(int id)
    {
        Table_Item_Tool.Info data = Managers.Table.m_Item_Tool.Get(id);

        if (data == null)
            return null;

        ToolItem tool = null;

        switch ((E_ToolType)data.m_iToolType)
        {
            case E_ToolType.KeyItems:
                break;
            case E_ToolType.MultiplayerItmes:
                break;
            case E_ToolType.Consumables:
                tool = new ToolItem_Consumable(id);
                break;
            case E_ToolType.Tools:
                break;
            case E_ToolType.Projectiles:
                tool = new ToolItem_Projectiles(id);
                break;
            case E_ToolType.Ammunition:
                break;
            case E_ToolType.Souls:
                break;
            case E_ToolType.BossSouls:
                break;
            case E_ToolType.Ore:
                break;
            case E_ToolType.Ashes:
                break;
            default:
                break;
        }

        return tool;
    }

    #region Boss

    public void ActivateBossFight(FogWall fogWall, AICharacterManager boss)
    {
        m_Boss = boss.GetComponent<EnemyBossManager>();

        // UI HP
        UIBossHealthBar bossHealthBar = Managers.GameUI.m_GameSceneUI.m_BossHealthBar;
        bossHealthBar.SetInfo(boss);

        // FLAG
        m_Boss.bossFightIsActive = true;
        m_Boss.bossHasBeenAwakened = true;

        // FogWall
        fogWall.ActivateFogWall();

        // Sound
        if(m_Boss.m_isBossSoundPlaying == false)
        {
            Managers.Sound.Play(m_Boss.m_audioClip, 1, Sound.Bgm);
            m_Boss.m_isBossSoundPlaying = true;
        }
    }

    public void BossHasBeenDefeated(FogWall fogWall)
    {
        // UI
        Managers.GameUI.m_GameSceneUI.m_BossHealthBar.Clear();

        // FLAG
        m_Boss.bossFightIsActive = false;
        m_Boss.bossHasBeenAwakened = false;

        // FogWall
        fogWall.DeactivateFogWall();

        // Sound
        if (m_Boss.m_isBossSoundPlaying == true)
        {
            Managers.Sound.RemoveBgm();
            m_Boss.m_isBossSoundPlaying = false;
        }
    }

    public void BossClear()
    {

    }

    #endregion

    #region Monster

    public void PlayerLockOnCheck()
    {
        // 카메라 락온 체크
        if (m_Player.inputHandler.lockOnFlag == false)
            return;

        // 근처에 다른 적이 있는지 체크
        // 있다면 왼쪽인지 오른쪽인지 체크
        m_Player.cameraHandler.HandleLockOn();
        if(m_Player.cameraHandler.m_trNearestLockOnTarget != null)
        {
            if (m_Player.cameraHandler.m_trleftLockTarget != null)
            {
                m_Player.cameraHandler.m_trCurrentLockOnTarget = m_Player.cameraHandler.m_trleftLockTarget;
            }
            else if (m_Player.cameraHandler.m_trRightLockTarget != null)
            {
                m_Player.cameraHandler.m_trCurrentLockOnTarget = m_Player.cameraHandler.m_trRightLockTarget;
            }
        }
        // 근처에 적이 없다면 해제
        else
        {
            m_Player.inputHandler.lockOnFlag = false;
            m_Player.cameraHandler.ClearLockOnTargets();
        }
    }

    #endregion

    #region Option

    public void GameQuit()
    {
        // Save & Load


        // Game Inner
        List<Item> items = m_Player.playerInventoryManager.FindItems((i) => i.m_eItemType == E_ItemType.MeleeWeapon);

        foreach (Item item in items)
        {
            ((WeaponItem)item).SetSound();
        }
    }
    #endregion
}
