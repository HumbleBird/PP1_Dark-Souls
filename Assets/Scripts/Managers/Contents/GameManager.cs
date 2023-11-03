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
    public int m_iPlayerDeadSoul = 0;

    // Inventory
    public int m_iInventoryCurrentSelectItemSlotNum = 0;
    public int m_iInventoryShowItemTapLeftNum = 0;
    public int m_iInventoryShowItemTapRightNum = 4;
    public int m_iInventoryCurrentSelectTapNum = 0;

    // Boss
    public EnemyBossManager m_Boss;
    public bool bossFightIsActive; // 현재 싸우고 있는가
    public bool bossHasBeenAwakened; // 이미 싸움 중에 한 번 죽어는가/이미 보스가 깨어져 있는가
    public bool bossHasBeenDefeated; // 보스가 죽었는가
    public bool m_isBossSoundPlaying = false;
    public GameObject m_goBossParticle;

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

        // Dead Souls
        if(m_goDeadSouls != null)
            Managers.Resource.Destroy(m_goDeadSouls.gameObject);
        m_goDeadSouls = Managers.Resource.Instantiate("Objects/Interact Object/Dead Soul").GetComponent<DeadSouls>();
        m_goDeadSouls.transform.position = m_Player.transform.position;
        m_goDeadSouls.m_iSoulsCount = m_iPlayerDeadSoul;
        m_iPlayerDeadSoul = 0;

        // UI 업데이트
        m_Player.m_GameSceneUI.RefreshUI(Define.E_StatUI.All);
        Managers.GameUI.CloseAllPopupUI();
        Managers.GameUI.m_GameSceneUI.m_BossHealthBar.Clear();

        // Boss
        if(bossHasBeenDefeated == false)
        {
            m_Boss.bossCombatStanceState.hasPhaseShifted = false;

            if (m_goBossParticle != null)
            {
                Managers.Resource.Destroy(m_goBossParticle);
                m_goBossParticle = null;
            }
        }

        // Player Input Clear
        m_Player.inputHandler.Clear();

        // Sound
        Managers.Sound.MuteBgm(m_Boss.m_audioClip);

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
        bossFightIsActive = true;
        bossHasBeenAwakened = true;

        // FogWall
        fogWall.ActivateFogWall();

        // Sound
        if(m_isBossSoundPlaying == false)
        {
            Managers.Sound.Play(m_Boss.m_audioClip, 1, Sound.Bgm);
            m_isBossSoundPlaying = true;
        }
    }

    public void BossHasBeenDefeated(FogWall fogWall)
    {
        // UI
        Managers.GameUI.m_GameSceneUI.m_BossHealthBar.Clear();

        // FLAG
        bossFightIsActive = false;
        bossHasBeenAwakened = false;

        // FogWall
        fogWall.DeactivateFogWall();

        // Sound
        if (m_isBossSoundPlaying == true)
        {
            Managers.Sound.MuteBgm(m_Boss.m_audioClip);
            m_isBossSoundPlaying = false;
        }
    }

    public void BossClear()
    {

    }

    #endregion
}
