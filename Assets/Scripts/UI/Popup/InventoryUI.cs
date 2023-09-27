using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UI_Popup
{
    // Left Panel - Iten Inventory
    // 1. 위의 각 아이템 부문별 아이콘을 클릭하면 해당 아이템만이 있는 아이템 인벤토리를 따로 보여줌.
    // 2. 각 화살표를 누르면 다음 아이템 부문 아이콘을 보여줌. ( 1, 2, 3, 4, 5) 에서 오른쪽 화살표를 누르면 (2, 3, 4, 5, 6) 이렇게 보여주고 싶음. 게임 오브젝트 setactive하면 됨.
    // 3. 아이템을 누르면 해당 자리에 셀렉트 자리를 지정하고, 해당 선택된 아이템의 정보를 Middle Panel의 아이템 창에서 보여줌. 마지막 서약 다음으로는 다시 도구로, 반대 방향도 마찬가지임
    // 4. 아이템 부문은 도구, 강화소재, 귀중품, 마법, 근접무기 ,원거리 무기, 촉매, 방패, 투구 ,갑옷, 장갑, 장화, 화살 / 볼트, 반지, 서약으로 나눠짐
    // 5, 아이템 부문을 바꿔도 셀렉트 자리가 그대로 이기 때문에 해당 자리의 아이템 정보를 보여 줘야 됨
    // 6. 각 아이템 별로 보여주는 정보가 다름.
    // 7. 클릭하면 장착할 지 말지를 팝업이 뜸. yes를 누르면 장비창이 뜨고 해당 여기서 해당 장비 칸을 누르면 교체하게 됨.
    // 8. 갑옷인데 무기를 클릭하면 소리만 나고, 교체가 되지 않게 한다.

    // Middle Panel - Item Description Detail
    // 1. left Panel에서 아이템을 클릭하면 이 창에서 아이템의 정보를 보여줌
    // 2. 아이템 이름, 아이템 아이콘을 보여주고. 아이템 종류에 따라 설명, 스텍 등을 보여줄 수 있음.

    // Right Panel - Player State
    // 플레이어의 스텟을 고정으로 보여줌.

    enum Texts
    {
        SoulText, // 현재 소울양


        // Left Panel - Iten Inventory

        // Item Part 부분
        ItemPartSubjectName, // 아이템 부문 별 이름
        // Item Inventory 부분
        ItemNameText, // 아이템 이름

        // Middle Panel - Item Description Detail

        // Right Panel - Player State
    }

    enum GameObjects
    {
        // Left Panel
        LeftArrow,
        RightArrow,
        InvnetoryContents, // 아이템 슬롯을 생성할 부모 오브젝트
    }

    enum Images
    {
        // Left Panel
        ItemBasePlateIcon,
        ItemIcon,
        ItemSelectIcon
    }

    PlayerManager player;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));

        gameObject.SetActive(false);
        return true;
    }

    private void Start()
    {
        player = Managers.Object.m_MyPlayer;
        GetText((int)Texts.SoulText).text = player.playerStatsManager.currentSoulCount.ToString();
        CreateInventorySlot(GetObject((int)GameObjects.InvnetoryContents), player.playerInventoryManager.m_Item.Count);
    }

    private void CreateInventorySlot(GameObject parent, int count)
    {
        foreach (Transform child in parent.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < count; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/InventoryItemSlot", parent.transform);
            InventoryItemSlot item = go.GetOrAddComponent<InventoryItemSlot>();

            item.SetInfo(player.playerInventoryManager.m_Item[i]);
        }
    }
}
